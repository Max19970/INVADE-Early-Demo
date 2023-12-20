using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class HUD : MonoBehaviour
{
    public PlayerController controller;

    public GameObject statusTextPrefab;
    public Camera mainCamera;
    public CinemachineImpulseSource impulseSource;

    public void AnimateHit(int value, string side)
    {
        // Animate hit effect if health went down
        Animator hitEffect = transform.Find("Hit Effect").GetComponent<Animator>();

        if (value < 0 && value >= -15)
        {
            hitEffect.Play("Slight Hit", 0, 0f);
            impulseSource.GenerateImpulse(new Vector3(0.2f * (side == "left" ? 1 : -1), 0, 0));
        }
        else if (value <= -16 && value >= -40)
        {
            hitEffect.Play("Medium Hit", 0, 0f);
            impulseSource.GenerateImpulse(new Vector3(0.4f * (side == "left" ? 1 : -1), 0, 0));
        }
        else if (value <= -41)
        {
            hitEffect.Play("Heavy Hit", 0, 0f);
            impulseSource.GenerateImpulse(new Vector3(0.6f * (side == "left" ? 1 : -1), 0, 0));
        }
    }

    public void AnimateItemPickup(int index)
    {
        transform.Find("Inventory").transform.Find("Item " + (index + 1).ToString()).gameObject.GetComponent<Animator>().Play("Item Picked Up", 0, 0);
    }

    public void AnimateItemSelection(int index)
    {
        Animator anim = transform.Find("Inventory").transform.Find("Item " + (index + 1).ToString()).gameObject.GetComponent<Animator>();
        transform.Find("Inventory").transform.Find("Item " + (index + 1).ToString()).gameObject.GetComponent<AudioSource>().Play();
        anim.Play("Item Held", 0, 0);
    }

    public void AnimateItemDeselection(int index)
    {
        Animator anim = transform.Find("Inventory").transform.Find("Item " + (index + 1).ToString()).gameObject.GetComponent<Animator>();
        anim.Play("Item Unheld", 0, 0);
    }

    public void AnimateItemUse(int index)
    {
        Animator anim = transform.Find("Inventory").transform.Find("Item " + (index + 1).ToString()).gameObject.GetComponent<Animator>();
        anim.Play("Item Used", 0, 0);
    }

    public void ShowStatusMessage(string text, Vector3 position, Vector3 offset)
    {
        StatusText statusText = ScriptableObject.CreateInstance<StatusText>();
        statusText.text = text;
        statusText.lookAt = position;
        statusText.offset = offset;
        statusText.cam = mainCamera;
        GameObject stextPrefab = statusTextPrefab;
        stextPrefab.GetComponent<DisplayText>().stext = statusText;
        Instantiate(stextPrefab, position, Quaternion.identity, transform);
    }

    public void UpdateHealthBar()
    {
        // Updates health bar on HUD
        int currentHealth = controller.currentHealth;
        Slider healthBarSlider = transform.Find("Health Bar").transform.Find("Bar").GetComponent<Slider>();
        Animator healthBarAnimator = transform.Find("Health Bar").GetComponent<Animator>();
        float someVelocity = 0f;
        float currentBarValue = Mathf.SmoothDamp(healthBarSlider.value, (float)currentHealth / (float)controller.maxHealth, ref someVelocity, 10 * Time.deltaTime);
        healthBarSlider.value = currentBarValue;
        healthBarAnimator.SetFloat("speed", 2 - (float)currentHealth / (float)controller.maxHealth);
        transform.Find("Health Bar").transform.Find("Value").GetComponent<TextMeshProUGUI>().text = controller.currentHealth.ToString() + " / " + controller.maxHealth.ToString();
    }

    public void UpdateInventory()
    {
        Dictionary<string, string>[] inventory = controller.inventory;
        // Dictionary<string, Sprite[]> spritesheets = new Dictionary<string, Sprite[]>();
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        // foreach (Dictionary<string, string> item in inventory) {
        //     if (item["name"] != "") {
        //         if (!spritesheets.ContainsKey(item["sprite"])) {
        //             spritesheets[item["sprite"]] = Resources.LoadAll<Sprite>(item["sprite"]);
        //             Debug.Log(spritesheets[item["sprite"]][0]);
        //         }
        //         foreach (Sprite sprite in spritesheets[item["sprite"]]) {
        //             if (sprite.name == item["name"]) {
        //                 sprites[item["name"]] = sprite;
        //             }
        //         }
        //     }
        // }
        Sprite[] pickups = Resources.LoadAll<Sprite>("Textures/Pickups/pickups");

        foreach (Dictionary<string, string> item in inventory)
        {
            foreach (Sprite sprite in pickups)
            {
                if (sprite.name == item["name"])
                {
                    sprites[item["name"]] = sprite;
                }
            }
        }

        for (int i = 0; i < 4; i++) {
            GameObject item = transform.Find("Inventory").transform.Find("Item " + (i + 1).ToString()).gameObject;
            GameObject spriteRenderer = item.transform.Find("Sprite").gameObject;
            if (inventory[i]["name"] != "") {
                spriteRenderer.GetComponent<Image>().sprite = sprites[inventory[i]["name"]];
                spriteRenderer.GetComponent<RectTransform>().sizeDelta = new Vector2(sprites[inventory[i]["name"]].rect.width, sprites[inventory[i]["name"]].rect.height);
            } else {
                spriteRenderer.GetComponent<Image>().sprite = null;
            }

            if (inventory[i]["selected"] == "true") {
                GameObject itemFrame = item.transform.Find("Frame").gameObject;
                itemFrame.GetComponent<Image>().color = new Color32(20, 146, 255, 50);
                itemFrame.transform.localScale = new Vector3(1.25f, 1.25f, 1);
            } else {
                GameObject itemFrame = item.transform.Find("Frame").gameObject;
                itemFrame.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                itemFrame.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateInventory();
    }
}
