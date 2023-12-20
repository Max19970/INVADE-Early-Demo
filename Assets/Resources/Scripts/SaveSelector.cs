using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSelector : MonoBehaviour
{
    // public MenuManager menuManager;
    // public GameManager gameManager;
    public GameObject manager;
    public SaveSlot saveSlot1;
    public SaveSlot saveSlot2;
    public SaveSlot saveSlot3;

    // private PauseMenu pauseMenu;

    public void Enable(string mode)
    {
        SetInteractable(true);
        GetComponent<Animator>().Play("Save Selector Appear", 0, 0);
        saveSlot1.SetMode(mode);
        saveSlot2.SetMode(mode);
        saveSlot3.SetMode(mode);
    }

    public void Disable()
    {
        SetInteractable(false);
        GetComponent<Animator>().Play("Save Selector Disappear", 0, 0);
        saveSlot1.SetMode("none");
        saveSlot2.SetMode("none");
        saveSlot3.SetMode("none");
        if (manager.GetComponent<MenuManager>() != null) manager.GetComponent<MenuManager>().SetInteractable(true);
        else if (manager.GetComponent<PauseMenu>() != null) manager.GetComponent<PauseMenu>().SetInteractable(true);
        // if (menuManager != null) menuManager.SetInteractable(true);
        // else pauseMenu.SetInteractable(true);
    }

    public void SetInteractable(bool interble, List<GameObject> exceptions = null)
    {
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            Button button = child.gameObject.GetComponent<Button>();
            if (button != null && (exceptions == null || (exceptions != null && !exceptions.Contains(child.gameObject))))
            {
                button.interactable = interble;
                if (child.gameObject.GetComponent<AudioSource>() != null) child.gameObject.GetComponent<AudioSource>().mute = !interble;
            }
        }
        // saveSlot1.gameObject.transform.Find("Select Button").gameObject.GetComponent<Button>().interactable = interactable;
        // saveSlot1.gameObject.transform.Find("Delete Button").gameObject.GetComponent<Button>().interactable = interactable;
        // saveSlot2.gameObject.transform.Find("Select Button").gameObject.GetComponent<Button>().interactable = interactable;
        // saveSlot2.gameObject.transform.Find("Delete Button").gameObject.GetComponent<Button>().interactable = interactable;
        // saveSlot3.gameObject.transform.Find("Select Button").gameObject.GetComponent<Button>().interactable = interactable;
        // saveSlot3.gameObject.transform.Find("Delete Button").gameObject.GetComponent<Button>().interactable = interactable;
        // transform.Find("Save Selector").Find("Back Button").gameObject.GetComponent<Button>().interactable = interactable;
    }

    void Start()
    {
        //if (transform.parent.gameObject.GetComponent<PauseMenu>() != null) pauseMenu = transform.parent.gameObject.GetComponent<PauseMenu>();
        // if (manager.GetComponent<SceneManager>() != null) transform.Find("Save Selector/Back Button").gameObject.GetComponent<Button>().onClick.AddListener(() => {
        //     // if (manager.savesShown) {manager.HideSaves(); manager.savesShown = false;}
        //     manager.HideSaves(); manager.savesShown = false;
        //     // else {manager.ShowSaves(); manager.savesShown = true;}
        // });
        // else transform.Find("Save Selector/Back Button").gameObject.GetComponent<Button>().onClick.AddListener(() => {
        //     Disable(); manager.savesShown = false;
        // });
        transform.Find("Save Selector/Back Button").gameObject.GetComponent<Button>().onClick.AddListener(() => {
            Disable();
        });
    }
}
