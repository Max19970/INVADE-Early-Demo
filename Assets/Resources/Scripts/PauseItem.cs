using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseItem : MonoBehaviour
{
    public int index;
    public Dictionary<string, string> myItem;
    public ItemDescription description;

    public void Select()
    {
        description.SetInfo(GetKeys(), transform.Find("Sprite").gameObject.GetComponent<Image>().sprite);
    }

    public Dictionary<string, string> GetKeys()
    {
        Dictionary<string, string> keys = new Dictionary<string, string>()
        {
            {"name", ""},
            {"description", ""},
        };

        switch (myItem["name"])
        {
            case "Gray Card":
                keys["name"] = "GrayCardNameText";
                keys["description"] = "GrayCardDescText";
                break;
            case "Nothing":
                keys["name"] = "NothingNameText";
                keys["description"] = "NothingDescText";
                break;
        }

        return keys;
    }

    void Awake()
    {
        myItem = transform.parent.gameObject.GetComponent<PauseInventory>().inventory[index];
    }

    void Start()
    {
        Sprite[] pickups = Resources.LoadAll<Sprite>("Textures/Pickups/pickups");
        GameObject spriteRenderer = transform.Find("Sprite").gameObject;
        Sprite mySprite = null;
        foreach (Sprite sprite in pickups)
        {
            if (sprite.name == myItem["name"])
            {
                mySprite = sprite;
            }
        }
        spriteRenderer.GetComponent<Image>().sprite = mySprite;
        spriteRenderer.GetComponent<RectTransform>().sizeDelta = new Vector2(mySprite.rect.width * (175 / 150), mySprite.rect.height * (175 / 150));
    }
}
