using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class StarterLang : MonoBehaviour
{
    public GameObject[] langs;
    private string[] codes = new string[2] {"en", "ru"};
    private bool done = false;

    public void ActiveLangs()
    {
        foreach (GameObject lang in langs)
        {
            lang.GetComponent<Button>().interactable = true;
        }
    }

    public void ChangeLang(string code)
    {
        done = true;
        StartCoroutine(Change(code));
    }

    public IEnumerator Change(string code)
    {
        yield return LocalizationSettings.InitializationOperation;
        PlayerPrefs.SetString("language", code);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[Array.IndexOf(codes, code)];
        if (done) GetComponent<Animator>().Play("StarterLang Hide", 0, 0);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(langs[0]);
        StartCoroutine(Change("en"));
    }
}
