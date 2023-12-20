using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilTurretAI : MonoBehaviour
{
    public int damage;
    public int health;
    public float followSpeed;
    public float bulletSpeed;
    public float shotCooldown;
    [Space(20)]
    public FieldOfView view;

    private GameObject gun;
    private AudioSource shotSound;
    private GameObject bulletPrefab;
    private GameObject smokePrefab;
    private Indicator indicator;
    private bool canShot = true;

    private void FollowTarget(GameObject target)
    {
        Vector2 targetCenterOff = new Vector2(target.transform.position.x, target.transform.position.y) + target.GetComponent<BoxCollider2D>().offset;
        // Vector3 targetCenter = new Vector3(targetCenterOff.x, targetCenterOff.y, gun.transform.position.z);

        float angle = Vector2.Angle(new Vector2(0, -1), new Vector2(targetCenterOff.x - gun.transform.position.x, targetCenterOff.y - gun.transform.position.y));
        angle = targetCenterOff.x < gun.transform.position.x ? -angle : angle;
        float someVelocity = 0f;
        float rotation = Mathf.SmoothDamp(gun.transform.eulerAngles.z > 180 ? gun.transform.eulerAngles.z - 360 : gun.transform.eulerAngles.z, angle, ref someVelocity, followSpeed * Time.deltaTime);
        rotation = rotation < 0 ? rotation + 360 : rotation;
        gun.transform.eulerAngles = new Vector3(
            gun.transform.eulerAngles.x,
            gun.transform.eulerAngles.y,
            rotation
        );

        if (gun.transform.Find("Line").gameObject.GetComponent<BoxCollider2D>().IsTouching(target.GetComponent<BoxCollider2D>()) && canShot) Shot(rotation);
    }

    private void Shot(float rotation)
    {
        Debug.Log("Shot");
        GameObject bullet = bulletPrefab;
        bullet.GetComponent<Bullet>().rotation = rotation;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        Instantiate(bullet, gun.transform.Find("Bullet Hole").position, Quaternion.identity, gun.transform);
        GameObject smoke = smokePrefab;
        Instantiate(smoke, gun.transform.Find("Bullet Hole").position, Quaternion.identity, gun.transform);
        shotSound.Play();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        canShot = false;
        //StartCoroutine(Reload(shotCooldown));
        indicator.Cycle(shotCooldown);
        yield return new WaitForSeconds(shotCooldown);
        canShot = true;
    }

    // private IEnumerator Reload(float rtime)
    // {
    //     bool done = false;
    //     while (!done)
    //     {
    //         float prevValue = indicator.value;
    //         float someVelocity = 0f;
    //         float currentValue = Mathf.SmoothDamp(prevValue, 1f, ref someVelocity, rtime, Mathf.Infinity, 0.03f);
    //         Debug.Log(currentValue);
    //         Debug.Log(rtime);
    //         indicator.ChangeFullness(currentValue);

    //         if (currentValue == 1f) done = true;
    //         yield return null;
    //     }
    // }

    void Awake()
    {
        gun = transform.Find("Gun").gameObject;
        shotSound = transform.Find("Shot Sound").gameObject.GetComponent<AudioSource>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        smokePrefab = Resources.Load<GameObject>("Prefabs/ShotSmoke");
        indicator = transform.Find("Indicator").gameObject.GetComponent<Indicator>();
    }

    void Start()
    {
        if (Gamestate.difficulty == "Easy")
        {
            damage = (int)Mathf.Round(damage * 0.8f);
            health = (int)Mathf.Round(health * 0.8f);
            followSpeed = 7;
        }
        else if (Gamestate.difficulty == "Normal")
        {
            followSpeed = 6;
        }
        else if (Gamestate.difficulty == "Hard")
        {
            damage = (int)Mathf.Round(damage * 1.2f);
            health = (int)Mathf.Round(health * 1.2f);
            followSpeed = 5;
        }
    }

    void Update()
    {
        FollowTarget(view.target);
    }
}
