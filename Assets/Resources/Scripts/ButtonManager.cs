using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public void Deselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetAnimatorActive(bool oper)
    {
        gameObject.GetComponent<Animator>().SetBool("Active", oper);
    }

    // public void UnactiveOthers()
    // {
    //     foreach (Animator animator in transform.parent.GetComponentsInChildren<Animator>())
    //     {
    //         if (animator.gameObject != gameObject) animator.SetBool("Active", false);
    //     }
    // }

    // public void SetBActive()
    // {
    //     gameObject.GetComponent<Animator>().SetBool("Active", true);
    // }

    // public void SettingsTrigger(GameObject settings)
    // {
    //     if (!settings.activeSelf)
    //     {
    //         settings.SetActive(true);
    //     }
    //     else
    //     {
    //         settings.GetComponent<Settings>().DisableBeauty();
    //     }
    // }

    // public void CloseSelector(GameObject selector)
    // {
    //     selector.GetComponent<SaveSelector>().SetInteractable(false);
    //     selector.GetComponent<Animator>().Play("Save Selector Disappear");
    // }

    // public void Exit(SceneManager sceneManager)
    // {
    //     StartCoroutine(sceneManager.Exit());
    // }
}
