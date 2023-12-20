using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private Animator animator;

    public void Fade(float seconds)
    {
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("Fade");
    }

    public void Unfade(float seconds)
    {
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("Unfade");
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
