using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ToolBox.Serialization;

public class SceneManager : MonoBehaviour
{
    public Fader fader;
    public MenuManager menuManager;
    public SaveSelector saveSelector;
    public Settings settingsManager;
    public MenuMusic menuMusic;
    public GameObject loadPrefab;

    public bool settingsShown = false;
    public bool savesShown = false;
    public float secondsToStart;

    public void Exit()
    {
        StartCoroutine(Exiting());
    }

    public IEnumerator Exiting()
    {
        // if (settingsManager.gameObject.activeSelf)
        // {
        //     settingsManager.DisableBeauty();
        // }
        // else
        // {
        //     settingsManager.gameObject.SetActive(true); ///
        //     settingsManager.SaveSettings(); ///
        //     settingsManager.gameObject.SetActive(false); ///
        // }
        //PlayerPrefs.SetInt("disclaimerShown", 0);
        DataSerializer.Save("disclaimerShown", false);
        menuManager.SetInteractable(false);
        settingsManager.SetInteractable(false);
        fader.Fade(1.5f);
        menuMusic.Fade(2f);
        yield return new WaitForSeconds(2f);
        Debug.Log("End!");
        Application.Quit();
    }

    public void ShowSettings()
    {
        settingsManager.Enable();
    }

    public void HideSettings()
    {
        settingsManager.Disable();
    }

    public void ShowSaves()
    {
        saveSelector.Enable("load");
    }

    public void HideSaves()
    {
        saveSelector.Disable();
    }

    // public void ProceedToSaves()
    // {
    //     if (settingsManager.gameObject.activeSelf) settingsManager.DisableBeauty();
    //     menuManager.SetInteractable(false);
    //     saveSelector.gameObject.SetActive(true);
    //     saveSelector.Enable("load");
    // }

    public IEnumerator LoadGame(string scene)
    {
        fader.Fade(1.5f);
        menuMusic.Fade(2f);
        yield return new WaitForSeconds(2f);

        GameObject loading = loadPrefab;
        loading.GetComponent<Canvas>().worldCamera = Camera.main;
        loading.GetComponent<Canvas>().sortingLayerName = "UI";
        loading = Instantiate(loading);
        loading.transform.Find("Circle").gameObject.GetComponent<Animator>().Play("LoadCircle Show", 0, 0);
        AsyncOperation sceneChange = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        sceneChange.allowSceneActivation = false;

        while (!(sceneChange.progress >= 0.9f))
        {
            Debug.Log(sceneChange.progress);
            yield return null;
        }

        StartCoroutine(loading.GetComponent<LoadScreen>().OnLoadOver(sceneChange));
        // UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    void Awake()
    {
        Application.targetFrameRate = 100;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Time.timeScale = 0f;

        GameObject loading = loadPrefab;
        loading.GetComponent<Canvas>().worldCamera = Camera.main;
        loading.GetComponent<Canvas>().sortingLayerName = "UI";
        loading = Instantiate(loadPrefab);
        loading.transform.Find("Circle").gameObject.GetComponent<Animator>().Play("LoadCircle Show", 0, 0);
        StartCoroutine(loading.GetComponent<LoadScreen>().OnLoadOver());
        yield return new WaitUntil(() => GameObject.Find("Loading(Clone)") == null);
        
        // PlayerPrefs.DeleteKey("language");
        // PlayerPrefs.DeleteKey("showDisclaimer");
        //if (!PlayerPrefs.HasKey("language"))
        if (DataSerializer.HasKey("language"))
        {
            GameObject langChoser = Resources.Load<GameObject>("Prefabs/Starter Language");
            langChoser = Instantiate(langChoser, GameObject.Find("Main Canvas").transform);
            langChoser.transform.SetSiblingIndex(langChoser.transform.parent.childCount - 1);
            while (GameObject.Find("Main Canvas/Starter Language(Clone)") != null)
            {
                yield return null;
            }
        }
        //if (!PlayerPrefs.HasKey("showDisclaimer") || (PlayerPrefs.GetInt("showDisclaimer") == 1 && PlayerPrefs.GetInt("disclaimerShown") != 1))
        if (!DataSerializer.HasKey("showDisclaimer") || (DataSerializer.Load<bool>("showDisclaimer") && !DataSerializer.Load<bool>("disclaimerShown")))
        {
            GameObject disclaimer = Resources.Load<GameObject>("Prefabs/Starter Disclaimer");
            disclaimer = Instantiate(disclaimer, GameObject.Find("Main Canvas").transform);
            disclaimer.transform.SetSiblingIndex(disclaimer.transform.parent.childCount - 1);
            while (GameObject.Find("Main Canvas/Starter Disclaimer(Clone)") != null)
            {
                yield return null;
            }
            //PlayerPrefs.SetInt("disclaimerShown", 1);
            DataSerializer.Save("disclaimerShown", true);
        }
        Time.timeScale = 1f;
        fader.Unfade(1f);
        menuMusic.Unfade(1.5f);
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
