using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [Header("Health Stats")]
    public int currentHealth;
    public int maxHealth;
    public GameObject damageEffect;
    public GameObject deathEffect;
    public UnityEvent afterDeath;

    private bool healthCheat;

    [Header("Movement Stats")]
    public float movementSpeed;
    public float wallSlideSpeed;
    public float jumpHeight;
    public float maxJumpHeight;
    public int jumpQuantity;
    public int maxJumpQuantity;
    public LayerMask floorLayer;
    public LayerMask wallLayer;

    [Header("Managers")]
    public HUD HUD;
    public GameManager gameManager;
    public MapManager mapManager;

    [Header("Sounds")]
    public AudioSource damageSound;

    [Space(20)]
    public AudioMixerGroup sfxGroup;
    public Dictionary<string, string>[] inventory;

    public Rigidbody2D myRigidBody;
    private BoxCollider2D myCollider;
    private float[] myInteractionArea;

    public float additionalForcesY;
    public float additionalForcesX;

    public bool sliding;
    public bool onFloor;
    public bool blocked;

    private void LoadInventory(string strInv)
    {
        List<string> inv = new List<string>(Gamestate.dataGlobal["player"]["inventory"].Split(new string[] {", "}, StringSplitOptions.None));
        inventory = new Dictionary<string, string>[]
        {
            new Dictionary<string, string>()
            {
                {"name", inv[0]},
                {"selected", inv[4] == "1" ? "true" : "false"}
            },
            new Dictionary<string, string>()
            {
                {"name", inv[1]},
                {"selected", inv[4] == "2" ? "true" : "false"}
            },
            new Dictionary<string, string>()
            {
                {"name", inv[2]},
                {"selected", inv[4] == "3" ? "true" : "false"}
            },
            new Dictionary<string, string>()
            {
                {"name", inv[3]},
                {"selected", inv[4] == "4" ? "true" : "false"}
            }
        };
    }

    private void LoadAbilities(string strAbil) {}

    public int GetActiveItemIndex()
    {
        int i = 0;
        foreach (Dictionary<string, string> item in inventory)
        {
            if (item["selected"] == "true") return i;
            i++;
        }
        throw new Exception("Selected item not found.");
    }

    public string GetActiveItemName()
    {
        foreach (Dictionary<string, string> item in inventory)
        {
            if (item["selected"] == "true") return item["name"];
        }
        throw new Exception("Selected item not found.");
    }

    public void SetWorldPosition(Vector3 point, float rotation)
    {
        myRigidBody.position = point;
        transform.localScale = new Vector3(rotation, 1, 1);
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = myRigidBody.velocity.y;

        if ((Input.GetKey(KeyCode.RightArrow) && additionalForcesX <= 0) || (Input.GetKey(KeyCode.LeftArrow) && additionalForcesX >= 0))
        {
            additionalForcesX = 0;
        }
        else if (additionalForcesX > 0.0001f)
        {
            additionalForcesX -= 0.1f;
        }
        else if (additionalForcesX < -0.0001f)
        {
            additionalForcesX += 0.1f;
        }
        else
        {
            additionalForcesX = 0;
        }
        myRigidBody.velocity = new Vector2((movementSpeed * moveHorizontal) + additionalForcesX, moveVertical);
    }

    private void Jump()
    {
        if (jumpQuantity > 0)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
            myRigidBody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            jumpHeight -= maxJumpHeight / 2 / maxJumpQuantity;
            jumpQuantity -= 1;
        }
    }

    private void WallJump()
    {
        if (jumpQuantity > 0)
        {
            myRigidBody.velocity = new Vector2(0, 0);
            myRigidBody.AddForce(new Vector2(transform.localScale.x == -1 ? -jumpHeight * 0.7f : jumpHeight * 0.7f, jumpHeight * 0.8f), ForceMode2D.Impulse);
            additionalForcesX = (transform.localScale.x == -1 ? -(jumpHeight - 0.02f) * 0.7f : (jumpHeight - 0.02f) * 0.7f) * 100;
            jumpHeight -= maxJumpHeight / 2 / maxJumpQuantity;
            jumpQuantity -= 1;
        }
    }

    private void WallSlide()
    {
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Mathf.Clamp(myRigidBody.velocity.y, -wallSlideSpeed, float.MaxValue));
    }

    public void SelectItem(int index)
    {
        for (int i = 0; i < 4; i++) {
            if (i != index) {
                if (inventory[i]["selected"] == "true") HUD.AnimateItemDeselection(i);
                inventory[i]["selected"] = "false";
            } else {
                if (inventory[i]["selected"] == "false") HUD.AnimateItemSelection(i);
                inventory[i]["selected"] = "true";
            }
        }
    }

    public void Interact()
    {
        string location = mapManager.currentLocation;
        Transform maps = GameObject.Find("Maps").transform;
        GameObject pickupsGroup = maps.Find(location).Find("Pickups").gameObject;
        GameObject interactorsGroup = maps.Find(location).Find("Interactors").gameObject;

        Dictionary<GameObject, float> objectsToInteract = new Dictionary<GameObject, float>(){};

        foreach (Transform child in pickupsGroup.GetComponentInChildren<Transform>())
        {
            if (child.Find("Interact Button").gameObject.activeSelf)
            {
                objectsToInteract[child.gameObject] = Vector2.Distance(child.position, new Vector2(myInteractionArea[0], myInteractionArea[1]));
            }
        }
        foreach (Transform child in interactorsGroup.GetComponentInChildren<Transform>())
        {
            if (child.Find("Interact Button").gameObject.activeSelf)
            {
                objectsToInteract[child.gameObject] = Vector2.Distance(child.position, new Vector2(myInteractionArea[0], myInteractionArea[1]));
            }
        }

        Dictionary<GameObject, float> ordered = objectsToInteract.OrderBy(x => -x.Value).ToDictionary(x => x.Key, x => x.Value);

        if (ordered.Count > 0)
        {
            GameObject objectToInteract = ordered.Keys.First();
            if (objectToInteract.CompareTag("Pickup")) PickUp(objectToInteract);
            else if (objectToInteract.CompareTag("Interactor")) UseItem(objectToInteract);
        }
    }

    public void PickUp(GameObject item)
    {
        foreach (Dictionary<string, string> slot in inventory)
        {
            if (slot["name"] == "Nothing")
            {
                //Gamestate.objectsToDestroy.Add(item.name);
                Gamestate.dataDeletedPickups[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][mapManager.currentLocation].Add(item.name);
                AudioSource.PlayClipAtPoint(item.GetComponent<AudioSource>().clip, new Vector3(0, 0, 0));
                Destroy(item);
                slot["name"] = item.GetComponent<PickupInfo>().pickupName;
                int index = Array.IndexOf(inventory, slot);
                HUD.AnimateItemPickup(index);
                break;
            }
        }
    }

    public void UseItem(GameObject interactor)
    {
        string componentName = interactor.GetComponent<InteractorInfo>().componentName;
        switch (componentName)
        {
            case "SimpleDoor":
                interactor.GetComponent<SimpleDoor>().Use(GetActiveItemName());
                //Gamestate.usedObjects.Add(interactor.name);
                break;
            default:
                throw new Exception("Interactor component with name " + componentName + " not found.");
        }
    }

    public void ChangeHealth(int value, string side)
    {
        currentHealth += value;
        if (healthCheat && value < 0) currentHealth -= value;
        if (value < 0) {
            if ((side == "left" && transform.localScale.x == 1) || (side == "right" && transform.localScale.x == -1)) GetComponent<Animator>().Play("Damage Left", -1, 0);
            else GetComponent<Animator>().Play("Damage Right", -1, 0);

            damageSound.clip = Resources.Load<AudioClip>("Audio/damageTaken" + UnityEngine.Random.Range(1, 10));
            AdvancedMethods.PlayClipAtPoint(damageSound.clip, new Vector3(transform.position.x, transform.position.y, 0), 1, sfxGroup);

            GameObject effect = Instantiate(damageEffect, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
            effect.transform.Rotate(new Vector3(-10, effect.transform.eulerAngles.y + (side == "left" ? 90 : -90), effect.transform.eulerAngles.z));
            effect.transform.SetParent(transform.parent);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);

            if (currentHealth <= 0)
            {
                GameObject effect1 = Instantiate(deathEffect, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                effect1.transform.Rotate(new Vector3(0, effect1.transform.eulerAngles.y + (side == "left" ? 90 : -90), effect1.transform.eulerAngles.z));
                effect1.transform.SetParent(transform.parent);
                Destroy(effect1, effect1.GetComponent<ParticleSystem>().main.duration);
                StartCoroutine(SetBlock(true));
                gameManager.SetBlock(true);
                afterDeath.Invoke();
                //StartCoroutine(gameManager.Death());
                Debug.Log("Dead");
                Destroy(gameObject);
            }
        }
        HUD.AnimateHit(value, side);
    }

    public IEnumerator SetBlock(bool block, float seconds = 0)
    {
        yield return new WaitForSeconds(seconds);
        blocked = block;
    }

    public void Load()
    {
        // List<string> position = new List<string>(Gamestate.dataGlobal["player"]["position"].Split(new string[] {", "}, StringSplitOptions.None));
        // Vector3 playerPosition = new Vector3(float.Parse(position[0], CultureInfo.InvariantCulture.NumberFormat),
        //                                      float.Parse(position[1], CultureInfo.InvariantCulture.NumberFormat),
        //                                      float.Parse(position[2], CultureInfo.InvariantCulture.NumberFormat));
        // float rotation = float.Parse(Gamestate.dataGlobal["player"]["rotation"], CultureInfo.InvariantCulture.NumberFormat);
        // SetWorldPosition(playerPosition, rotation);
        currentHealth = int.Parse(Gamestate.dataGlobal["player"]["currentHealth"]);
        LoadInventory(Gamestate.dataGlobal["player"]["inventory"]);
        LoadAbilities(Gamestate.dataGlobal["player"]["abilities"]);
    }

    public Dictionary<string, string> Save()
    {
        Dictionary<string, string> result = new Dictionary<string, string>()
        {
            {"position", transform.position.x.ToString().Replace(",", ".") + ", " + transform.position.y.ToString().Replace(",", ".") + ", " + transform.position.z.ToString().Replace(",", ".")},
            {"rotation", transform.localScale.z.ToString().Replace(",", ".")},
            {"currentHealth", currentHealth.ToString()},
            {"inventory", inventory[0]["name"] + ", " + inventory[1]["name"] + ", " + inventory[2]["name"] + ", " + inventory[3]["name"] + ", " + (GetActiveItemIndex() + 1).ToString()},
            {"abilities", "Unknown, Unknown, Unknown, Unknown, Unknown"}
        };

        return result;
    }

    void Start()
    {
        // currentHealth = int.Parse(Gamestate.playerInfo[4]);
        // maxHealth = int.Parse(Gamestate.playerInfo[5]);

        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myInteractionArea = new float[4] {0f, 0.6f, 0f, 0.7f};

        // LoadInventory();
        // //SetWorldPosition(new string[3] {Gamestate.playerInfo[0], Gamestate.playerInfo[1], Gamestate.playerInfo[2]});
        // SetWorldPosition(new Vector3(float.Parse(Gamestate.playerInfo[0], CultureInfo.InvariantCulture.NumberFormat),
        //                              float.Parse(Gamestate.playerInfo[1], CultureInfo.InvariantCulture.NumberFormat),
        //                              float.Parse(Gamestate.playerInfo[2], CultureInfo.InvariantCulture.NumberFormat)),
        //                  float.Parse(Gamestate.playerInfo[3], CultureInfo.InvariantCulture.NumberFormat));
    }

    void Update()
    {
        if (!blocked)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectItem(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectItem(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SelectItem(3);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        if (Input.GetKeyDown(KeyCode.H)) {healthCheat = !healthCheat; currentHealth = maxHealth; Debug.Log("Health Cheat: " + (healthCheat ? "Enabled" : "Disabled"));}
    }

    void FixedUpdate()
    {
        if (!blocked)
        {
            if (!sliding) Move();
            else WallSlide();

            Collider2D wall = Physics2D.OverlapCircle(transform.Find("Body").position, Mathf.Abs(transform.localScale.x) / 2f + 0.2f, wallLayer);
            if (wall && wall is BoxCollider2D && !onFloor)
            {
                sliding = true;
                if (wall.gameObject.transform.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
                else transform.localScale = new Vector3(-1, 1, 1);
                jumpHeight = maxJumpHeight;
                jumpQuantity = maxJumpQuantity;
            }
            else
            {
                sliding = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpHeight = maxJumpHeight;
            jumpQuantity = maxJumpQuantity;
            sliding = false;
            onFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}
