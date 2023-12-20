using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ToolBox.Serialization;

public class Settings : MonoBehaviour
{
    private Animator animator;

    private PageSelector pageSelector;
    // public AudioPage audioPage;
    // public GamePage gamePage;
    // public OtherPage otherPage;
    public Toggle defaultLang;

    // public void Disable()
    // {
    //     gameObject.SetActive(false);
    // }

    // public void DisableBeauty()
    // {
    //     GetComponent<Settings>().SaveSettings();
    //     GetComponent<Animator>().Play("Disappear");
    // }

    public void Enable()
    {
        SetInteractable(true);
        animator.Play("Appear", 0, 0);
        pageSelector.Init();
    }

    public void Disable()
    {
        SetInteractable(false);
        animator.Play("Disappear", 0, 0);
    }

    public void SetInteractable(bool interble)
    {
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            Button button = child.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = interble;
                if (child.gameObject.GetComponent<AudioSource>() != null) child.gameObject.GetComponent<AudioSource>().mute = !interble;
            }
        }
    }

    public void ResetSettings()
    {
        foreach (GameOptions option in gameObject.GetComponentsInChildren<GameOptions>())
        {
            option.ResetMe();
        }
        foreach (AudioSlider audioSlider in gameObject.GetComponentsInChildren<AudioSlider>())
        {
            audioSlider.ResetMe();
        }
        defaultLang.isOn = true;
        // PlayerPrefs.SetInt("showDisclaimer", 1);
        DataSerializer.Save("showDisclaimer", true);
    }

    // public void SaveSettings()
    // {
    //     Dictionary<string, string> gamePageSettings = gamePage.GetData();
    //     Dictionary<string, string> audioPageSettings = audioPage.GetData();
    //     Dictionary<string, string> otherPageSettings = otherPage.GetData();
    //     string[] saveScreens = SaveFileCurrent.saveScreens;

    //     // Dictionary<string, Dictionary<string, string>> settings = new Dictionary<string, Dictionary<string, string>>()
    //     // {
    //     //     {"game", new Dictionary<string, string>(){}},
    //     //     {"audio", new Dictionary<string, string>(){}},
    //     //     {"other", new Dictionary<string, string>(){}},
    //     //     {"settings", String.Join(", ", SaveFileCurrent.saveScreens)}
    //     // };
        
    //     // foreach (KeyValuePair<string, string> entry in gamePageSettings)
    //     // {
    //     //     settings["game"][entry.Key] = entry.Value;
    //     // }
    //     // foreach (KeyValuePair<string, string> entry in audioPageSettings)
    //     // {
    //     //     settings["audio"][entry.Key] = entry.Value;
    //     // }
    //     // foreach (KeyValuePair<string, string> entry in otherPageSettings)
    //     // {
    //     //     settings["other"][entry.Key] = entry.Value;
    //     // }

    //     SaveSettings settings = new SaveSettings(gamePageSettings, audioPageSettings, otherPageSettings, saveScreens);
    //     SaveLoadSystem.SaveOptions(settings);
    // }

    // public void LoadSettings()
    // {
    //     SaveSettings settings = SaveLoadSystem.LoadOptions();

    //     gamePage.LoadData(settings.gamePageSettings);
    //     audioPage.LoadData(settings.audioPageSettings);
    //     otherPage.LoadData(settings.otherPageSettings);
    //     SaveFileCurrent.saveScreens = settings.saveScreens;
    // }

    void Awake()
    {
        animator = GetComponent<Animator>();
        pageSelector = transform.Find("Page Selector").GetComponent<PageSelector>();
        // gamePage = transform.Find("Game Page").GetComponent<GamePage>();
        // audioPage = transform.Find("Audio Page").GetComponent<AudioPage>();
        // otherPage = transform.Find("Other Page").GetComponent<OtherPage>();
    }
    
    // void Start()
    // {
    //     LoadSettings();
    // }
}
