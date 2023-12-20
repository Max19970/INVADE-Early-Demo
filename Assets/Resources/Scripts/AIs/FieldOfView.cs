using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject target;
    public GameObject defaultTarget;

    void Awake()
    {
        target = defaultTarget;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if (collider.gameObject.CompareTag("Player")) target = collider.gameObject;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) target = defaultTarget;
    }
}
