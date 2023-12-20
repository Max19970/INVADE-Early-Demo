using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSmoke : MonoBehaviour
{
    public float lifeTime;

    void Awake()
    {
        lifeTime = GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constantMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(270, 0, 0);
        transform.SetParent(transform.parent.parent, true);
        transform.SetSiblingIndex(0);
        Destroy(gameObject, lifeTime);   
    }
}
