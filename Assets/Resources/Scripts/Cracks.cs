using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Cracks : MonoBehaviour
{
    public AudioSource locationAudio;
    public GameFader fader;
    public MapManager mapManager;
    private Animator animator;
    private AudioSource audioSource;
    private CinemachineImpulseSource impulseSource;
    public int count = 0;
    public float secondsToBoot;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        locationAudio.clip = Resources.Load<AudioClip>("Audio/alarmSound");
        StartCoroutine(mapManager.player.SetBlock(true));
        yield return new WaitForSeconds(secondsToBoot);
        yield return WaitForKeyPress(KeyCode.Mouse0);
        Break();
        yield return WaitForKeyPress(KeyCode.Mouse0);
        Break();
        yield return WaitForKeyPress(KeyCode.Mouse0);
        Break();
        yield return WaitForKeyPress(KeyCode.Mouse0);
        FinalBreak();
    }

    public void Break()
    {
        impulseSource.GenerateImpulse();
        count++;
        if (count == 1) Destroy(transform.Find("Subtitles").gameObject);
        if (count > 2) mapManager.EnableSound(0.25f);
        audioSource.Play();
        animator.Play("Alarm For Cracks " + count.ToString());
        fader.BreakEffect(0.5f);
    }

    public void FinalBreak()
    {
        animator.SetBool("break", true);
        transform.Find("Cracks Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Textures/HUD/cracks")[0];
        audioSource.clip = Resources.Load<AudioClip>("Audio/crackFinal");
        audioSource.Play();
        fader.BreakEffect(-1f);
        //fader.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        mapManager.ChangeLocation("Laborotory1", new Vector3(-7.11f, -1.245f, -1.746137f), 1, "None", 1f);
        StartCoroutine(mapManager.player.SetBlock(false, 0.7f));
        Destroy(gameObject, 3.161f);
        StartCoroutine(Destroying(1f));
        Gamestate.dataDeletedProps[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]["Start"].Add(gameObject.name);
        //Debug.Log(Gamestate.dataDeletedProps[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]["Start"][0]);
        //Gamestate.playerInfo[6] = "True";
    }

    public IEnumerator Destroying(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        fader.BreakEffect(1.5f);
    }

    public IEnumerator WaitForKeyPress(KeyCode key)
    {
        bool done = false;
        while(!done) {     
            if(Input.GetKeyDown(key)) {
                done = true;
            }
            yield return null;
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = transform.Find("Audio Source").GetComponent<AudioSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // void OnDisable()
    // {
    //     fader.BreakEffect(0.5f);
    // }
}
