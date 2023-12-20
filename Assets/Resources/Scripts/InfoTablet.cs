using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoTablet : MonoBehaviour
{
    public bool blocked;
    private List<GameObject> myButtons;

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
        blocked = !interble;
    }

    public void Uninteract()
    {
        SetInteractable(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        myButtons = new List<GameObject>() {};
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            Button button = child.gameObject.GetComponent<Button>();
            if (button != null)
            {
                myButtons.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (!blocked)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EventSystem.current.SetSelectedGameObject(transform.Find("Inventory").Find("Item 1").gameObject);
                transform.Find("Inventory").Find("Item 1").GetComponent<Button>().onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EventSystem.current.SetSelectedGameObject(transform.Find("Inventory").Find("Item 2").gameObject);
                transform.Find("Inventory").Find("Item 2").GetComponent<Button>().onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EventSystem.current.SetSelectedGameObject(transform.Find("Inventory").Find("Item 3").gameObject);
                transform.Find("Inventory").Find("Item 3").GetComponent<Button>().onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                EventSystem.current.SetSelectedGameObject(transform.Find("Inventory").Find("Item 4").gameObject);
                transform.Find("Inventory").Find("Item 4").GetComponent<Button>().onClick.Invoke();
            }
        }

        if (!myButtons.Contains(EventSystem.current.currentSelectedGameObject))
        {
            transform.Find("Description").GetComponent<ItemDescription>().ResetInfo();
        }
    }
}
