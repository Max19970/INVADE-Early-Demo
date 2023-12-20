using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameObject playButton;
    private GameObject settingsButton;
    private GameObject exitButton;

    public void SetInteractable(bool interactable)
    {
        playButton.GetComponent<Button>().interactable = interactable;
        playButton.transform.Find("Hover Sound").gameObject.GetComponent<AudioSource>().mute = !interactable;
        settingsButton.GetComponent<Button>().interactable = interactable;
        settingsButton.transform.Find("Hover Sound").gameObject.GetComponent<AudioSource>().mute = !interactable;
        exitButton.GetComponent<Button>().interactable = interactable;
        exitButton.transform.Find("Hover Sound").gameObject.GetComponent<AudioSource>().mute = !interactable;
    }

    void Awake()
    {
        playButton = transform.Find("Play Button").gameObject;
        settingsButton = transform.Find("Options Button").gameObject;
        exitButton = transform.Find("Exit Button").gameObject;
    }
    
    void Start()
    {
        settingsButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager manager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
            if (manager.settingsShown) {manager.HideSettings(); manager.settingsShown = false;}
            else {manager.ShowSettings(); manager.settingsShown = true;}
        });
    }
}
