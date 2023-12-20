using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public PlayerController player;
    public RuntimeAnimatorController effectAnimator;
    public string effectName;
    public bool isEffectEnabled;
    public AudioClip myMusic;
    public AudioClip myMusicSecond;
    public AudioClip mySound;
    public AudioClip mySoundSecond;
    public bool soundAtEnter;
    public bool isEntering;

    public string smySound;
    public string smySoundSecond;
    public string smyMusic;
    public string smyMusicSecond;

    public Dictionary<string, string> Save()
    {
        Dictionary<string, string> result = new Dictionary<string, string>()
        {
            {"effectName", effectName},
            {"isEffectEnabled", isEffectEnabled ? "1" : "0"},
            {"mySound", smySound},
            {"mySoundSecond", smySoundSecond},
            {"myMusic", smyMusic},
            {"myMusicSecond", smyMusicSecond},
            {"soundAtEnter", soundAtEnter ? "1" : "0"},
            {"isEntering", isEntering ? "1" : "0"}
        };

        return result;
    }

    public void Load()
    {
        Dictionary<string, string> data = Gamestate.dataLocations[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][gameObject.name];
        effectName = data["effectName"];
        isEffectEnabled = data["isEffectEnabled"] == "1";
        mySound = Resources.Load<AudioClip>(data["mySound"]);
        mySoundSecond = Resources.Load<AudioClip>(data["mySoundSecond"]);
        myMusic = Resources.Load<AudioClip>(data["myMusic"]);
        myMusicSecond = Resources.Load<AudioClip>(data["myMusicSecond"]);
        soundAtEnter = data["soundAtEnter"] == "1";
        isEntering = data["isEntering"] == "1";

        smySound = data["mySound"];
        smySoundSecond = data["mySoundSecond"];
        smyMusic = data["myMusic"];
        smyMusicSecond = data["myMusicSecond"];
    }

    public void DeletePickups()
    {
        List<string> data = Gamestate.dataDeletedPickups[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][gameObject.name];
        foreach (string pickup in data)
        {
            Destroy(transform.Find("Pickups/" + pickup).gameObject);
        }
    }

    public void RestoreInteractors()
    {
        List<string> data = Gamestate.dataUsedInteractors[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][gameObject.name];
        foreach (string interactor in data)
        {
            GameObject interactorObj = transform.Find("Interactors/" + interactor).gameObject;
            // Debug.Log(interactorObj.gameObject.name);
            // for (int i = 0; i < paired.Count; i++) {Debug.Log(paired[i]);}
            // Debug.Log(interactor);
            switch (interactorObj.GetComponent<InteractorInfo>().componentName)
            {
                case "SimpleDoor":
                    interactorObj.GetComponent<SimpleDoor>().InstaUse();
                    interactorObj.GetComponent<SimpleDoor>().myTrigger.RestoreMapTrigger();
                    break;
                default:
                    throw new Exception("Interactor component with name " + interactorObj.GetComponent<InteractorInfo>().componentName + " not found.");
            }
        }
    }

    public void DeleteProps()
    {
        List<string> data = Gamestate.dataDeletedProps[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][gameObject.name];
        foreach (string prop in data)
        {
            Destroy(transform.Find("Props/" + prop).gameObject);
        }
    }

    public void EnableEffect()
    {
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.gameObject.AddComponent<Animator>();
                Animator animator = spriteRenderer.gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = effectAnimator;
                animator.Play(effectName);
            }
        }
        foreach (Transform child in player.gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.gameObject.AddComponent<Animator>();
                Animator animator = spriteRenderer.gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = effectAnimator;
                animator.Play(effectName);
            }
        }
        isEffectEnabled = true;
    }

    public void DisableEffect()
    {
        foreach (Transform child in GetComponentsInChildren(typeof(Transform)))
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.gameObject.GetComponent<Animator>().runtimeAnimatorController == effectAnimator)
            {
                Destroy(spriteRenderer.gameObject.GetComponent<Animator>());
            }
        }
        foreach (Transform child in player.gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.gameObject.GetComponent<Animator>().runtimeAnimatorController == effectAnimator)
            {
                Destroy(spriteRenderer.gameObject.GetComponent<Animator>());
            }
        }
        isEffectEnabled = false;
    }

    void Awake()
    {
        effectAnimator = Resources.Load<RuntimeAnimatorController>("Animations/Effects/Effect Animator");
    }
}
