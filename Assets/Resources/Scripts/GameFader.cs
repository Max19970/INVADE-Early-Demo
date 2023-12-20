using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFader : MonoBehaviour
{
    private Animator animator;

    public void Fade(float seconds)
    {
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("Fade", 0, 0);
    }

    public void Unfade(float seconds)
    {
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("Unfade", 0, 0);
    }

    public void BreakEffect(float seconds)
    {
        if (seconds > 0) animator.SetFloat("speed", 1 / seconds);
        else animator.SetFloat("speed", 0);
        animator.Play("Break Effect", 0, 0);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
