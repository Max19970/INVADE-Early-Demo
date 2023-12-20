using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;

public class DifficultyPage : MonoBehaviour
{
    private GameObject preview1;
    private GameObject preview2;
    private GameObject desc;
    
    public bool shown;

    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        shown = true;
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        shown = false;
    }

    public void SetDifficulty(string difficulty)
    {
        // preview1.GetComponent<Image>().sprite = blabla
        // preview2.GetComponent<Image>().sprite = blabla
        desc.GetComponent<LocalizeStringEvent>().StringReference.SetReference("UI Text", difficulty + "DescriptionText");;
    }

    // public void Expand()
    // {
    //     if (!preview.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dif Preview Show")) preview.GetComponent<Animator>().Play("Dif Preview Show", 0, 0);
    //     if (!desc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dif Desc Show")) desc.GetComponent<Animator>().Play("Dif Desc Show", 0, 0);
    // }

    // public void Unexpand()
    // {
    //     preview.GetComponent<Animator>().Play("Dif Preview Hide", 0, 0);
    //     desc.GetComponent<Animator>().Play("Dif Desc Hide", 0, 0);
    // }

    void Awake()
    {
        preview1 = transform.Find("Preview 1").gameObject;
        preview2 = transform.Find("Preview 2").gameObject;
        desc = transform.Find("Description").gameObject;
    }

    // void Update()
    // {
    //     if ((EventSystem.current.currentSelectedGameObject != dname) && shown)
    //     {
    //         shown = false;
    //         dname.GetComponent<Animator>().SetBool("Active", false);
    //         //dname.GetComponent<Button>().interactable = true;
    //         Unexpand();
    //     }
    // }

    // void Start()
    // {
    //     dname.GetComponent<Button>().onClick.AddListener(() => {shown = true; dname.GetComponent<Animator>().SetBool("Active", true); Expand();});
    // }
}
