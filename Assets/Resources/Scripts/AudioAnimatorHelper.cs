using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimatorHelper : MonoBehaviour
{
    private AudioSource source;

    public void TurnOff()
    {
        Debug.Log("stop");
        source.Stop();
    }

    public void TurnOn()
    {
        Debug.Log("asd");
        source.Play();
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
}
