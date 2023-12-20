using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    public GameObject name;
    public GameObject text;
    public GameObject image;

    public void SetInfo(Dictionary<string, string> keys, Sprite sprite)
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        name.GetComponent<LocalizeStringEvent>().StringReference.SetReference("ItemDescriptions", keys["name"]);
        text.GetComponent<LocalizeStringEvent>().StringReference.SetReference("ItemDescriptions", keys["description"]);
        name.GetComponent<TextMeshProUGUI>().text = name.GetComponent<TextMeshProUGUI>().text.Replace("\\n", "\n");
        text.GetComponent<TextMeshProUGUI>().text = text.GetComponent<TextMeshProUGUI>().text.Replace("\\n", "\n");
        Image spriteRenderer = image.GetComponent<Image>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width * (350 / 150), sprite.rect.height * (350 / 150));
    }

    public void ResetInfo()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }

    void Awake()
    {
        name = transform.Find("Name").gameObject;
        text = transform.Find("Text").gameObject;
        image = transform.Find("Image").gameObject;
    }
    
    void Start()
    {
        ResetInfo();
    }
}
