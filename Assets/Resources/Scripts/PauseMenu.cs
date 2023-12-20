using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;
    public AudioMixerGroup sfxGroup;

    private Animator animator;
    private bool blocked = true;
    private GameObject menu;
    private GameObject locInfo;
    private GameObject infoTablet;
    private GameObject settingsMenu;
    private GameObject saveSelector;
    private bool equipShowed;
    private bool settingsShowed;

    public void Close()
    {
        animator.Play("Pause Close", 0, 0);
        SetInteractable(false);
        gameManager.ResumeEverything(true);
        if (settingsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Appear")) settingsMenu.GetComponent<Settings>().Disable();
        transform.Find("Menu").Find("Resume Button").gameObject.GetComponent<AudioSource>().mute = true;
        AdvancedMethods.PlayClipAtPoint(transform.Find("Menu").Find("Resume Button").gameObject.GetComponent<AudioSource>().clip, new Vector3(transform.position.x, transform.position.y, 0), 1, sfxGroup);
    }

    public void Quit()
    {
        SetInteractable(false);
        if (settingsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Appear")) settingsMenu.GetComponent<Settings>().Disable();
        StartCoroutine(gameManager.Quit());
    }

    public void SetInteractable(bool interble)
    {
        AdvancedMethods.PlayClipAtPoint(transform.Find("Menu").Find("Resume Button").gameObject.GetComponent<AudioSource>().clip, new Vector3(transform.position.x, transform.position.y, 0), 1, sfxGroup);
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            Button button = child.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = interble;
                if (child.gameObject.GetComponent<AudioSource>() != null) child.gameObject.GetComponent<AudioSource>().mute = !interble;
            }
        }
        blocked = !interble;
    }

    public void Equipment()
    {
        if (equipShowed) {StartCoroutine(HideEquip()); equipShowed = false;}
        else {StartCoroutine(ShowEquip()); equipShowed = true;}
    }

    public void Settings()
    {
        if (settingsShowed) {StartCoroutine(HideSettings()); settingsShowed = false;}
        else {StartCoroutine(ShowSettings()); settingsShowed = true;}
    }

    public IEnumerator ShowEquip()
    {
        if (!settingsShowed)
        {
            locInfo.GetComponent<Animator>().Play("Loc Info Disappear", 0, 0);
            menu.GetComponent<Animator>().Play("Move Left", 0, 0);
            yield return new WaitForSecondsRealtime(1/6f);
            infoTablet.SetActive(true);
            infoTablet.GetComponent<InfoTablet>().SetInteractable(true);
        }
        else
        {
            settingsMenu.GetComponent<Settings>().Disable();
            settingsMenu.GetComponent<Settings>().SetInteractable(false);
            //yield return new WaitForSecondsRealtime(2/6f);
            infoTablet.SetActive(true);
            infoTablet.GetComponent<InfoTablet>().SetInteractable(true);
            settingsShowed = false;
        }
    }

    public IEnumerator HideEquip()
    {
        infoTablet.GetComponent<Animator>().Play("Tablet Disappear", 0, 0);
        infoTablet.GetComponent<InfoTablet>().SetInteractable(false);
        yield return new WaitForSecondsRealtime(1/6f);
        menu.GetComponent<Animator>().Play("Move Right", 0, 0);
        yield return new WaitForSecondsRealtime(1/6f);
        locInfo.GetComponent<Animator>().Play("Loc Info Appear", 0, 0);
    }

    public IEnumerator ShowSettings()
    {
        if (!equipShowed)
        {
            locInfo.GetComponent<Animator>().Play("Loc Info Disappear", 0, 0);
            menu.GetComponent<Animator>().Play("Move Left", 0, 0);
            yield return new WaitForSecondsRealtime(1/6f);
            settingsMenu.GetComponent<Settings>().Enable();
        }
        else
        {
            infoTablet.GetComponent<Animator>().Play("Tablet Disappear", 0, 0);
            infoTablet.GetComponent<InfoTablet>().SetInteractable(false);
            //yield return new WaitForSecondsRealtime(2/6f);
            settingsMenu.GetComponent<Settings>().Enable();
            equipShowed = false;
        }
    }

    public IEnumerator HideSettings()
    {
        settingsMenu.GetComponent<Settings>().Disable();
        yield return new WaitForSecondsRealtime(1/6f);
        menu.GetComponent<Animator>().Play("Move Right", 0, 0);
        yield return new WaitForSecondsRealtime(1/6f);
        locInfo.GetComponent<Animator>().Play("Loc Info Appear", 0, 0);
    }

    public void ShowSaveSelectorLoad()
    {
        //if (settingsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Appear")) settingsMenu.GetComponent<Settings>().Disable();
        SetInteractable(false);
        saveSelector.SetActive(true);
        saveSelector.GetComponent<SaveSelector>().Enable("load");
    }

    public void ShowSaveSelectorSave()
    {
        //if (settingsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Appear")) settingsMenu.GetComponent<Settings>().Disable();
        SetInteractable(false);
        saveSelector.SetActive(true);
        saveSelector.GetComponent<SaveSelector>().Enable("save");
    }

    public void Unblock()
    {
        blocked = false;
    }

    public void DestroyMe()
    {
        gameManager.SetBlock(false);
        Destroy(gameObject);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        menu = transform.Find("Menu").gameObject;
        locInfo = transform.Find("Location Info").gameObject;
        infoTablet = transform.Find("Info Tablet").gameObject;
        settingsMenu = transform.Find("Settings Menu").gameObject;
        saveSelector = transform.Find("Save Selection Window").gameObject;
    }

    void Update()
    {
        if (!blocked)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
    }
}
