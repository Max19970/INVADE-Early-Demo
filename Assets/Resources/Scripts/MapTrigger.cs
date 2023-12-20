using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public MapManager mapManager;
    public string location;
    public Vector3 point;
    public float rotation;
    public string transitionBy;
    public float seconds;
    public Location myLocation;
    public bool activateSecondSound;
    public bool activateSecondMusic;

    public void Enable()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Front Plan";
        GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void Disable()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public IEnumerator SetSecondSound(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        myLocation.mySound = myLocation.mySoundSecond;
        myLocation.smySound = myLocation.smySoundSecond;
        myLocation.mySoundSecond = null;
        myLocation.smySoundSecond = "";
        activateSecondSound = false;
    }

    public IEnumerator SetSecondMusic(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        myLocation.myMusic = myLocation.myMusicSecond;
        myLocation.smyMusic = myLocation.smyMusicSecond;
        myLocation.myMusicSecond = null;
        myLocation.smyMusicSecond = "";
        activateSecondMusic = false;
    }

    public void RestoreMapTrigger()
    {
        string[] data = Gamestate.dataMapTriggers[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][myLocation.gameObject.name][gameObject.transform.parent.gameObject.name].Split(new string[] {", "}, StringSplitOptions.None);
        activateSecondSound = data[0] == "1";
        activateSecondMusic = data[1] == "1";
    }

    public string SaveMapTrigger()
    {
        string data = (activateSecondSound ? "1" : "0") + ", " + (activateSecondMusic ? "1" : "0");
        return data;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            mapManager.ChangeLocation(location, point, rotation, transitionBy, seconds);
            if (activateSecondSound)
            {
                StartCoroutine(SetSecondSound(seconds));
            }
            if (activateSecondMusic)
            {
                StartCoroutine(SetSecondMusic(seconds));
            }
        }
    }
}
