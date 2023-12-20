using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    public Toggle option;
    public string code;
    private string[] languages = new string[2] {"en", "ru"};
    // public int index;

    // public void Select()
    // {
    //     StartCoroutine(ChangeLanguage());
    // }

    public IEnumerator SetLanguage()
    {
        // GameObject selectedOption = languageOptions.ActiveToggles().FirstOrDefault().gameObject;
        // string code = selectedOption.GetComponent<LanguageSwitcher>().code;
        yield return LocalizationSettings.InitializationOperation;
        PlayerPrefs.SetString("language", code);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[Array.IndexOf(languages, code)];
    }

    // public IEnumerator ChangeLanguage()
    // {
    //     yield return LocalizationSettings.InitializationOperation;
    //     LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    // }

    void Awake()
    {
        option.onValueChanged.AddListener((bool _) => {if (_) StartCoroutine(SetLanguage());});
    }

    void Start()
    {
        if (code == PlayerPrefs.GetString("language")) {option.isOn = true; StartCoroutine(SetLanguage());}
    }
}
