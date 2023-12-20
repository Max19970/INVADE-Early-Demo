using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameFader fader;
    public PlayerController player;
    public GameObject pauseMenuPrefab;
    public GameObject localAudio;
    public GameObject globalAudio;

    public float secondsToStartUp;
    public bool blocked;

    IEnumerator Start()
    {
        AudioListener.volume = 0;
        SaveLoadInGame.Load();
        SetBlock(true);
        yield return new WaitForSeconds(secondsToStartUp);
        SetBlock(false);
        //Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        AudioListener.volume = 1;
        fader.Unfade(1f);
        // if (GetComponent<MapManager>().location.soundAtEnter) ResumeAudio(1);
    }

    public void SetBlock(bool block)
    {
        blocked = block;
    }

    public void StopEverything(bool audiotoo)
    {
        Time.timeScale = 0;
        StartCoroutine(player.SetBlock(true, 0f));
        if (audiotoo) StartCoroutine(PauseAudio(1/3f));
    }

    public void ResumeEverything(bool audiotoo)
    {
        Time.timeScale = 1;
        StartCoroutine(player.SetBlock(false, 0f));
        if (audiotoo) ResumeAudio(1/3f);
    }

    public IEnumerator PauseAudio(float seconds)
    {
        globalAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        globalAudio.GetComponent<Animator>().Play("Fade Audio");
        localAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        localAudio.GetComponent<Animator>().Play("Fade Audio");
        yield return new WaitForSecondsRealtime(seconds);
        globalAudio.GetComponent<AudioSource>().Pause();
        localAudio.GetComponent<AudioSource>().Pause();
    }

    public void ResumeAudio(float seconds)
    {
        globalAudio.GetComponent<AudioSource>().Play();
        localAudio.GetComponent<AudioSource>().Play();
        globalAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        globalAudio.GetComponent<Animator>().Play("Unfade Audio");
        localAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        localAudio.GetComponent<Animator>().Play("Unfade Audio");
    }

    public void DeathStart()
    {
        StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        Debug.Log("Beginnng");
        yield return new WaitForSeconds(2f);
        Debug.Log("WTF");
        fader.Fade(0.5f);
        StartCoroutine(PauseAudio(0.5f));
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public IEnumerator Quit()
    {
        fader.Fade(2f);
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // public void DestroyObjects()
    // {
    //     foreach (PickupInfo pickup in GameObject.Find("Maps").GetComponentsInChildren(typeof(PickupInfo)))
    //     {
    //         if (Gamestate.objectsToDestroy.Contains(pickup.gameObject.name)) Destroy(pickup.gameObject);
    //     }
    //     Debug.Log(Gamestate.playerInfo[6] == "True");
    //     if (Gamestate.playerInfo[6] == "True") Destroy(GameObject.Find("Maps").transform.Find("Start").Find("Props").Find("Cracks"));
    // }

    // public void UseObjects()
    // {
    //     foreach (InteractorInfo inter in GameObject.Find("Maps").GetComponentsInChildren(typeof(InteractorInfo)))
    //     {
    //         if (Gamestate.objectsToDestroy.Contains(inter.gameObject.name))
    //         {
    //             switch (inter.componentName)
    //             {
    //                 case "SimpleDoor":
    //                     inter.gameObject.GetComponent<SimpleDoor>().InstaUse();
    //                     break;
    //             }
    //         }
    //     }
    // }

    public IEnumerator LoadGame(string scene)
    {
        fader.Fade(1.5f);
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;

        GameObject loading = Resources.Load<GameObject>("Prefabs/Loading");
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
    }

    void Update()
    {
        if (!blocked)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GetComponent<MapManager>().isChanging && GetComponent<MapManager>().currentLocation != "Start")
            {
                StartCoroutine(player.SetBlock(true, 0f));
                StopEverything(true);
                SetBlock(true);
                GameObject pauseMenu = pauseMenuPrefab;
                pauseMenu.GetComponent<PauseMenu>().gameManager = GetComponent<GameManager>();
                pauseMenu.GetComponent<Canvas>().worldCamera = Camera.main;
                pauseMenu.GetComponent<Canvas>().sortingLayerName = "UI";
                pauseMenu.transform.Find("Save Selection Window").GetComponent<SaveSelector>().manager = pauseMenu;
                pauseMenu.transform.Find("Save Selection Window/Save Selector/Save 1").GetComponent<SaveSlot>().manager = gameObject;
                pauseMenu.transform.Find("Save Selection Window/Save Selector/Save 2").GetComponent<SaveSlot>().manager = gameObject;
                pauseMenu.transform.Find("Save Selection Window/Save Selector/Save 3").GetComponent<SaveSlot>().manager = gameObject;
                pauseMenu = Instantiate(pauseMenu);
                Camera.main.GetComponent<TakeScreenshot>().pauseMenu = pauseMenu;
            }
        }
    }
}
