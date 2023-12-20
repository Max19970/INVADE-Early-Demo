using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float rotation;
    public float speed;
    public int damage;

    // public List<Collider2D> ignoreCol;

    private GameObject trail;

    void Awake()
    {
        // foreach (Collider2D obj in ignoreCol)
        // {
        //     Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), obj, true);
        // }
        trail = transform.Find("Trail").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
        Vector2 vel = new Vector2(speed * Mathf.Cos((rotation - 90) * Mathf.PI / 180), speed * Mathf.Sin((rotation - 90) * Mathf.PI / 180));
        GetComponent<Rigidbody2D>().velocity = vel;
        transform.SetParent(transform.parent.parent, true);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().ChangeHealth(-damage, transform.position.x > collider.gameObject.transform.position.x ? "right" : "left");
            // Destroy(gameObject, 0.1f);
        }

        trail.transform.SetParent(trail.transform.parent.parent, true);
        trail.GetComponent<ParticleHelper>().StopEmission(0.01f);
        Destroy(trail, trail.GetComponent<ParticleSystem>().main.duration + trail.GetComponent<ParticleSystem>().main.startLifetime.constantMax + 0.01f);
        //else
        //{
            Destroy(gameObject, 0.01f);
        //}
    }
}
