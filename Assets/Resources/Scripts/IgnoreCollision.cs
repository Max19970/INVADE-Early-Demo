using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PolygonCollider2D>() != null) {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        }
        if (GetComponent<CircleCollider2D>() != null) {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
        }
        if (GetComponent<BoxCollider2D>() != null) {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }
}
