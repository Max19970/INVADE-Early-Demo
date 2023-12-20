using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUps
{
    Unknown,
}

public class PowerUpUI : MonoBehaviour
{
    public PowerUps myPower = PowerUps.Unknown;
    public ItemDescription description;

    public void Select()
    {
        description.SetInfo(GetKeys(), transform.Find("Icon").gameObject.GetComponent<Image>().sprite);
    }

    public Dictionary<string, string> GetKeys()
    {
        Dictionary<string, string> keys = new Dictionary<string, string>()
        {
            {"name", ""},
            {"description", ""}
        };

        switch (myPower)
        {
            case PowerUps.Unknown:
                keys["name"] = "UnknownNameText";
                keys["description"] = "UnknownDescText";
                break;
        }

        return keys;
    }
}
