using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInventory : MonoBehaviour
{
    public Dictionary<string, string>[] inventory;

    void Awake()
    {
        inventory = GameObject.Find("Player").GetComponent<PlayerController>().inventory;
    }
}
