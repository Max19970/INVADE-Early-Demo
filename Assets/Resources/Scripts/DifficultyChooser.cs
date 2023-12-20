using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DifficultyChooser : MonoBehaviour
{
    public GameObject description;
    public GameObject confirm;
    public GameObject[] others;

    public bool active;

    public void Expand()
    {
        GetComponent<Animator>().SetBool("Active", true);
        description.GetComponent<Animator>().Play("Dif Desc Show", 0, 0);
        confirm.GetComponent<Button>().interactable = true;
        active = true;
        foreach (GameObject other in others)
        {
            //other.transform.SetSiblingIndex(2);
            other.GetComponent<Button>().interactable = false;
        }
    }

    public void Unexpand()
    {
        GetComponent<Animator>().SetBool("Active", false);
        description.GetComponent<Animator>().Play("Dif Desc Hide", 0, 0);
        confirm.GetComponent<Button>().interactable = false;
        active = false;
        foreach (GameObject other in others)
        {
            //other.transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
            other.GetComponent<Button>().interactable = true;
        }
    }

    // void Awake()
    // {
    //     page = transform.parent.Find("Difficulty Page").gameObject.GetComponent<DifficultyPage>();
    // }

    // void Update()
    // {
    //     GameObject current = EventSystem.current.currentSelectedGameObject as GameObject;
    //     if ((current != gameObject) && page.shown)
    //     {
    //         GetComponent<Animator>().SetBool("Active", false);
    //         if (current.TryGetComponent(out DifficultyChooser chooser)) {}
    //         else transform.parent.gameObject.GetComponent<DifficultyChoice>().ChangeDifficulty("None");
    //         //if (current.GetComponent<DifficultyChooser>() == null) transform.parent.gameObject.GetComponent<DifficultyChoice>().ChangeDifficulty("None");
    //         page.Hide();
    //     }
    // }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            if (active) {Unexpand(); EventSystem.current.SetSelectedGameObject(null);} else Expand();
        });
    }
}
