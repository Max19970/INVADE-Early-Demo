using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public void Fade(float seconds)
    {
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("AFade", 0, 0);
    }

    public void Unfade(float seconds)
    {
        audioSource.Play();
        animator.SetFloat("speed", 1 / seconds);
        animator.Play("AUnfade", 0, 0);
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.time = 40.073f;
    }
}
