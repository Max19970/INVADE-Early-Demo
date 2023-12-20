using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;

public class Subtitles : MonoBehaviour
{
    public string textKey;
    public bool glow;

    void Start()
    {
        transform.Find("Text (TMP)").GetComponent<LocalizeStringEvent>().StringReference.SetReference("UI Text", textKey);
        if (glow)
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("Glow", 0, 0);
        }
    }
}
