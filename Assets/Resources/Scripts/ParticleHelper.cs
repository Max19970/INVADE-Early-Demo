using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHelper : MonoBehaviour
{
    public void StopEmission(float time)
    {
        StartCoroutine(StopEm(time));
    }

    IEnumerator StopEm(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<ParticleSystem>().enableEmission = false;
    }
}
