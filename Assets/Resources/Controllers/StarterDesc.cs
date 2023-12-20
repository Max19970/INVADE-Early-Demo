using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolBox.Serialization;

public class StarterDesc : MonoBehaviour
{
    public Toggle dontShow;
    private bool skipped = false;

    public void Hide()
    {
        GetComponent<Animator>().Play("Starter Desc Hide", 0, 0);
        DataSerializer.Save("showDisclaimer", dontShow.isOn ? 0 : 1);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void BlockSkip()
    {
        skipped = true;
    }

    public void SkipStart()
    {
        GetComponent<Animator>().Play("Starter Desc Show", 0, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 60);
        skipped = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !skipped) SkipStart();
    }
}
