using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GamePage : MonoBehaviour
{

    // private string[] languages = new string[2] {"en", "ru"};

    // public string GetLanguage()
    // {
    //     Toggle selectedOption = languageOptions.ActiveToggles().FirstOrDefault();
    //     return selectedOption.gameObject.GetComponent<LanguageSwitcher>().code;
    // }

    // public Dictionary<string, string> GetData()
    // {
    //     return new Dictionary<string, string>()
    //     {
    //         {"languageCode", languageCode}
    //     };
    // }

    // public void SetLanguage(string code)
    // {
    //     // GameObject selectedOption = languageOptions.ActiveToggles().FirstOrDefault().gameObject;
    //     // string code = selectedOption.GetComponent<LanguageSwitcher>().code;
    //     PlayerPrefs.SetString("language", code);
    //     LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[Array.IndexOf(languages, code)];
    // }

    // public void LoadData(Dictionary<string, string> data)
    // {
    //     Debug.Log(data["languageCode"]);
    //     foreach (LanguageSwitcher option in languageOptions.transform.GetComponentsInChildren<LanguageSwitcher>())
    //     {
    //         if (option.code == data["languageCode"])
    //         {
    //             option.Select();
    //             break;
    //         }
    //     }
    // }

    // void Start()
    // {
    //     if (!PlayerPrefs.HasKey("language")) PlayerPrefs.SetString("language", "en");
    //     SetLanguage(PlayerPrefs.GetString("language"));
    // }

    // void Update()
    // {
    //     languageCode = GetLanguage();
    // }
}
