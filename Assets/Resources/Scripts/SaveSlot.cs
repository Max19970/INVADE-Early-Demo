using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using ToolBox.Serialization;

public class SaveSlot : MonoBehaviour
{
    public GameObject manager;
    public GameObject difChoserPrefab;
    // public Settings settingsMenu;
    // public SceneManager sceneManager;
    // public GameManager gameManager;
    public Button myButton;
    public Preview preview;
    public TextMeshProUGUI undertext;
    public int index;

    public void SetMode(string mode)
    {
        if (mode == "load") myButton.onClick.AddListener(delegate{LoadSave(index);});
        else if (mode == "save") myButton.onClick.AddListener(delegate{SaveSave(index);});
        else myButton.onClick.RemoveAllListeners();
    }

    public void LoadSave(int index)
    {
        SaveGamestate state = SaveLoadSystem.LoadState(index);
        //Debug.Log(state.dataGlobal["player"]["position"]);

        // Gamestate.playerInfo = state.playerInfo;
        // Gamestate.playerInventory = state.playerInventory;
        // Gamestate.objectsToDestroy = state.objectsToDestroy;
        // Gamestate.usedObjects = state.usedObjects;
        // Gamestate.powerUps = state.powerUps;
        // Gamestate.humansToDestroy = state.humansToDestroy;
        // Gamestate.humanLocations = state.humanLocations;
        // Debug.Log(Gamestate.playerInfo[0]);
        Gamestate.difficulty = state.difficulty;
        Gamestate.dataGlobal = state.dataGlobal;
        Gamestate.dataLocations = state.dataLocations;
        Gamestate.dataDeletedPickups = state.dataDeletedPickups;
        Gamestate.dataUsedInteractors = state.dataUsedInteractors;
        Gamestate.dataMapTriggers = state.dataMapTriggers;
        Gamestate.dataDeletedProps = state.dataDeletedProps;
        Gamestate.currentScene = state.currentScene;
        Gamestate.currentLocation = state.currentLocation;
        SaveFileCurrent.index = index;

        if (Gamestate.difficulty == "None")
        {
            GameObject choser = difChoserPrefab;
            choser.GetComponent<DifficultyChoice>().manager = manager;
            Instantiate(difChoserPrefab, transform.parent.parent);
        }
        else
        {
            transform.parent.parent.gameObject.GetComponent<SaveSelector>().SetInteractable(false);
            if (manager.GetComponent<SceneManager>() != null) StartCoroutine(manager.GetComponent<SceneManager>().LoadGame(state.currentScene));
            else if (manager.GetComponent<GameManager>() != null) StartCoroutine(manager.GetComponent<GameManager>().LoadGame(state.currentScene));
        }
        // if (sceneManager != null) StartCoroutine(sceneManager.LoadGame());
        // else StartCoroutine(gameManager.LoadGame());
    }

    public void SaveSave(int index)
    {
        // SavePlayer();

        // Dictionary<string, List<string>> state = new Dictionary<string, List<string>>()
        // {
        //     {"playerInfo", Gamestate.playerInfo},
        //     {"playerInventory", Gamestate.playerInventory},
        //     {"objectsToDestroy", Gamestate.objectsToDestroy},
        //     {"usedObjects", Gamestate.usedObjects},
        //     {"powerUps", Gamestate.powerUps},
        //     {"humansToDestroy", Gamestate.humansToDestroy},
        //     {"humanLocations", Gamestate.humanLocations}
        // };

        // SaveLoadSystem.SaveState(state, index);
        SaveGamestate state = SaveLoadInGame.Save(); // !!!
        SaveLoadSystem.SaveState(state, index); // !!!
        preview.UpdateImage(index);
        //PlayerPrefs.SetString("saveSlot" + index.ToString() + "text", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\n" + "Difficulty: " + Gamestate.difficulty);
        PlayerPrefs.SetString("saveSlot" + index.ToString() + "text", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\n" + "Difficulty: " + Gamestate.difficulty); // !!!
        undertext.text = PlayerPrefs.GetString("saveSlot" + index.ToString() + "text");
        // bool settState = settingsMenu.gameObject.activeSelf;
        // if (!settState) settingsMenu.gameObject.SetActive(true);
        // settingsMenu.SaveSettings();
        // if (!settState) settingsMenu.gameObject.SetActive(false);
    }

    // public void SavePlayer()
    // {
    //     PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
    //     Gamestate.playerInfo[0] = player.gameObject.transform.position.x.ToString().Replace(",", ".");
    //     Gamestate.playerInfo[1] = player.gameObject.transform.position.y.ToString().Replace(",", ".");
    //     Gamestate.playerInfo[2] = player.gameObject.transform.position.z.ToString().Replace(",", ".");
    //     Gamestate.playerInfo[3] = player.gameObject.transform.localScale.x.ToString();
    //     Gamestate.playerInfo[4] = player.currentHealth.ToString();
    //     Gamestate.playerInfo[5] = player.maxHealth.ToString();
    //     Gamestate.playerInfo[7] = player.mapManager.currentLocation;

    //     Gamestate.playerInventory[0] = player.inventory[0]["name"];
    //     Gamestate.playerInventory[1] = player.inventory[1]["name"];
    //     Gamestate.playerInventory[2] = player.inventory[2]["name"];
    //     Gamestate.playerInventory[3] = player.inventory[3]["name"];
    //     Gamestate.playerInventory[4] = player.GetActiveItemIndex().ToString();
    // }

    public void DeleteSave(int index)
    {
        SaveLoadSystem.DeleteState(index); // !!!
        // PlayerPrefs.SetString("saveSlot" + index.ToString(), "");
        // PlayerPrefs.SetString("saveSlot" + index.ToString() + "text", "");
        PlayerPrefs.SetString("saveSlot" + index.ToString(), "");
        PlayerPrefs.SetString("saveSlot" + index.ToString() + "text", "");
        preview.SetImage("Default", index);
        undertext.text = "";
    }

    void Start()
    {
        //if (sceneManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //if (SaveFileCurrent.saveScreens[index - 1] != "") preview.SetImage(SaveFileCurrent.saveScreens[index - 1]);
        //if (PlayerPrefs.GetString("saveSlot" + index.ToString()) != "") preview.SetImage(PlayerPrefs.GetString("saveSlot" + index.ToString()));
        if (PlayerPrefs.GetString("saveSlot" + index.ToString()) != "") preview.SetImage(PlayerPrefs.GetString("saveSlot" + index.ToString()));
        else preview.SetImage("Default", index);

        //if (PlayerPrefs.GetString("saveSlot" + index.ToString() + "text") != "")
        if (PlayerPrefs.GetString("saveSlot" + index.ToString() + "text") != "")
        {
            //undertext.text = PlayerPrefs.GetString("saveSlot" + index.ToString() + "text");
            undertext.text = PlayerPrefs.GetString("saveSlot" + index.ToString() + "text");
        }
        // Debug.Log(SaveFileCurrent.saveScreens[index - 1]);
    }
}
