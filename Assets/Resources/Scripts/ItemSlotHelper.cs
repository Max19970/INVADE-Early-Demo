using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotHelper : MonoBehaviour
{
    public PlayerController controller;

    public void SelectItem()
    {
        controller.SelectItem(transform.GetSiblingIndex());
    }
}
