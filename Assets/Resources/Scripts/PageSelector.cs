using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSelector : MonoBehaviour
{
    public GameObject[] pageButtons;
    public int page;

    private Settings settingsManager;
    private UnderSlide underSlide;

    public void Init()
    {
        EventSystem.current.SetSelectedGameObject(pageButtons[0]);
        pageButtons[0].GetComponent<Animator>().SetBool("Active", true);
        SwitchPage();
    }

    public void UnActiveOthers()
    {
        for (int i = 0; i < pageButtons.Length; i++)
        {
            if (i != page) pageButtons[i].GetComponent<Animator>().SetBool("Active", false);
        }
    }

    public void Active(Animator button)
    {
        button.SetBool("Active", true);
    }

    public void SwitchPage()
    {
        page = Array.IndexOf(pageButtons, EventSystem.current.currentSelectedGameObject);
        for (int i = 2; i < settingsManager.transform.childCount - 1; i++)
        {
            settingsManager.transform.GetChild(i).gameObject.GetComponent<CanvasGroup>().alpha = (page + 2) == i ? 1 : 0;
            settingsManager.transform.GetChild(i).gameObject.GetComponent<CanvasGroup>().interactable = (page + 2) == i ? true : false;
            settingsManager.transform.GetChild(i).gameObject.GetComponent<CanvasGroup>().blocksRaycasts = (page + 2) == i ? true : false;
        }
        UnActiveOthers();
        underSlide.Move(EventSystem.current.currentSelectedGameObject.transform.localPosition.x);
    }

    void Awake()
    {
        settingsManager = gameObject.transform.parent.gameObject.GetComponent<Settings>();
        underSlide = transform.Find("UnderSlider").gameObject.GetComponent<UnderSlide>();
    }
}
