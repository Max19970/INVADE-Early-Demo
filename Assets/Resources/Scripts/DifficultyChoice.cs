using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChoice : MonoBehaviour
{
    //public string difficulty { get => difficulty; set { difficulty = value; ImChanged(); } }
    public GameObject manager;
    public string difficulty;
    //public DifficultyChooser[] choices;
    //public Button confirmButton;

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
    }

    public void ChangeDifficulty(string dif)
    {
        difficulty = dif;
    }

    public void Confirm()
    {
        SetInteractable(false);
        Gamestate.difficulty = difficulty;
        if (manager.GetComponent<SceneManager>() != null) StartCoroutine(manager.GetComponent<SceneManager>().LoadGame(Gamestate.currentScene));
        else if (manager.GetComponent<GameManager>() != null) StartCoroutine(manager.GetComponent<GameManager>().LoadGame(Gamestate.currentScene));
    }

    public void Back()
    {
        GetComponent<Animator>().Play("Dif Choice Hide", 0, 0);
        StartCoroutine(DestoryInRealtime(gameObject, 1/6f));
    }

    public IEnumerator DestoryInRealtime(GameObject obj, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(obj);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {Back();}
    }
}
