using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class MapManager : MonoBehaviour
{
    public string currentLocation;
    public Location location;
    public PlayerController player;
    public Animator transition;
    public GameFader fader;
    public GameObject cinemachineCamera;
    public AudioSource globalAudio;
    public AudioSource localAudio;
    public GameObject locationInfoPrefab;

    public bool isChanging;

    public void ChangeLocation(string locationName, Vector3 point, float rotation, string transitionBy, float seconds = 0)
    {
        isChanging = true;

        StartCoroutine(ChangePhase1(locationName, point, rotation, transitionBy, seconds));
    }

    public IEnumerator ChangePhase1(string locationName, Vector3 point, float rotation, string transitionBy, float seconds = 0)
    {
        if (transitionBy != "Fader" && transitionBy != "Effect" && transitionBy != "None")
            throw new Exception("There is no transition object with ID " + transitionBy);

        if (transitionBy == "Effect")
        {
            transition.Play("Start Transition", 0, 0);
        }
        else if (transitionBy == "Fader")
        {
            fader.Fade(seconds);
        }

        currentLocation = locationName;
        Location loc = GameObject.Find("Maps").transform.Find(locationName).GetComponent<Location>();

        if (location != null)
        {
            if ((loc != null) && (localAudio.clip != loc.mySound)) StartCoroutine(DisableSound(seconds));
            if ((loc != null) && (globalAudio.clip != loc.myMusic)) StartCoroutine(DisableMusic(seconds));
        }

        yield return new WaitForSeconds(seconds);
        if (location != null && location.effectName != "None") location.DisableEffect();
        location = loc;
        ChangePhase2(point, rotation, transitionBy, seconds);
    }

    public void ChangePhase2(Vector3 point, float rotation, string transitionBy, float seconds = 0)
    {
        if (location != null)
        {
            if (location.effectName != "None") location.EnableEffect();
            if ((localAudio.clip != location.mySound) && location.soundAtEnter)
            {
                localAudio.clip = location.mySound;
                EnableSound(seconds);
            }
            if ((globalAudio.clip != location.myMusic) && location.soundAtEnter)
            {
                globalAudio.clip = location.myMusic;
                EnableMusic(seconds);
            }
            cinemachineCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = location.gameObject.transform.Find("Confiner").GetComponent<PolygonCollider2D>();
        }

        player.SetWorldPosition(point, rotation);

        StartCoroutine(ChangePhase3(transitionBy, seconds));
    }

    public IEnumerator ChangePhase3(string transitionBy, float seconds = 0)
    {
        if (transitionBy == "Effect")
        {
            transition.Play("End Transition", 0, 0);
        }
        else if (transitionBy == "Fader")
        {
            fader.Unfade(seconds);
        }
        if (location.isEntering)
        {
            GameObject popup = locationInfoPrefab;
            popup.GetComponent<LocationInfo>().sectionName = SectionNames.TheLab;
            popup.GetComponent<LocationInfo>().popUpOnStart = true;
            popup.GetComponent<LocationInfo>().popUpOnStart = true;
            foreach (Transform child in popup.GetComponentsInChildren(typeof(Transform)))
            {
                Image image = child.GetComponent<Image>();
                TextMeshProUGUI textmesh = child.GetComponent<TextMeshProUGUI>();
                if (image != null) image.color = new Color(1, 1, 1, 0);
                if (textmesh != null) textmesh.color = new Color(1, 1, 1, 0);
            }
            Instantiate(popup, transition.gameObject.transform.parent);

            foreach (Transform map in GameObject.Find("Maps").GetComponentsInChildren(typeof(Transform)))
            {
                if (map.GetComponent<Location>() != null) map.GetComponent<Location>().isEntering = false;
            }
        }

        yield return new WaitForSeconds(seconds);
        isChanging = false;
    }

    public void EnableMusic(float seconds)
    {
        globalAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        globalAudio.GetComponent<Animator>().Play("Unfade Audio");
        globalAudio.Play();
    }

    public IEnumerator DisableMusic(float seconds)
    {
        globalAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        globalAudio.GetComponent<Animator>().Play("Fade Audio");
        yield return new WaitForSeconds(seconds);
        globalAudio.Stop();
    }

    public void EnableSound(float seconds)
    {
        localAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        localAudio.GetComponent<Animator>().Play("Unfade Audio");
        localAudio.Play();
    }

    public IEnumerator DisableSound(float seconds)
    {
        localAudio.GetComponent<Animator>().SetFloat("speed", 1 / seconds);
        localAudio.GetComponent<Animator>().Play("Fade Audio");
        yield return new WaitForSeconds(seconds);
        localAudio.Stop();
    }

    void Awake()
    {
        // currentLocation = Gamestate.playerInfo[7];
        // location = GameObject.Find("Maps").transform.Find(Gamestate.playerInfo[7]).GetComponent<Location>();
        // if (location != null)
        // {
        //     if (location.effectName != "None") location.EnableEffect();
        //     localAudio.clip = location.mySound;
        //     globalAudio.clip = location.myMusic;
        //     if (location.soundAtEnter) EnableSound(0.5f);
        //     if (location.soundAtEnter) EnableMusic(0.5f);
        // }
        // cinemachineCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = location.gameObject.transform.Find("Confiner").GetComponent<PolygonCollider2D>();
    }
}
