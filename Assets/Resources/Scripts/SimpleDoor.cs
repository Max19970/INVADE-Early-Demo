using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public string side;
    public SimpleDoor otherSideDoor;

    public bool opened = false;

    private BoxCollider2D hitbox;
    private SpriteRenderer spriteRenderer;
    private AudioSource mySound;
    public MapTrigger myTrigger;
    private string neededItem = "Any";

    public void Use(string item)
    {
        if (!opened) {Open(); otherSideDoor.Open(false);}
        else {Close(); otherSideDoor.Close();}
    }

    public void InstaUse()
    {
        Debug.Log("hi");
        Use(neededItem);
    }

    public void Open(bool use = true)
    {
        Sprite[] interactorSprites = Resources.LoadAll<Sprite>("Textures/Game/Interactors");
        if (side == "From")
        {
            spriteRenderer.sprite = null;
            spriteRenderer.sortingOrder = 12;
        }
        else if (side == "On")
        {
            float prevRectWidth = spriteRenderer.sprite.bounds.size.x * Mathf.Abs(transform.localScale.x);
            foreach (Sprite sprite in interactorSprites)
            {
                if (sprite.name == "doorOpened1") {spriteRenderer.sprite = sprite; break;}
            }
            transform.position += new Vector3((transform.localScale.x < 0 ? (prevRectWidth - 0.074f) : -(prevRectWidth - 0.074f)), 0.15f - 0.019f, 0);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            myTrigger.gameObject.transform.position -= new Vector3((transform.localScale.x > 0 ? (prevRectWidth - 0.074f) : -(prevRectWidth - 0.074f)), 0.15f - 0.019f, 0);
            myTrigger.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            Transform intBut = transform.Find("Interact Button");
            intBut.localScale = new Vector3(-intBut.localScale.x, intBut.localScale.y, intBut.localScale.z);
        }
        mySound.clip = Resources.Load<AudioClip>("Audio/doorOpened");
        mySound.Play();
        myTrigger.Enable();
        hitbox.enabled = false;
        opened = true;
        GetComponent<InteractorInfo>().used = use;
    }

    public void Close(bool use = false)
    {
        Sprite[] interactorSprites = Resources.LoadAll<Sprite>("Textures/Game/Interactors");
        foreach (Sprite sprite in interactorSprites)
        {
            if (sprite.name == "doorClosed") {spriteRenderer.sprite = sprite; break;}
        }
        if (side == "From")
        {
            spriteRenderer.sortingOrder = 2;
        }
        else if (side == "On")
        {
            float rectWidth = spriteRenderer.sprite.bounds.size.x * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position -= new Vector3((transform.localScale.x < 0 ? (rectWidth - 0.074f) : -(rectWidth - 0.074f)), 0.15f - 0.019f, 0);
            myTrigger.gameObject.transform.position += new Vector3((transform.localScale.x > 0 ? (rectWidth - 0.074f) : -(rectWidth - 0.074f)), 0.15f - 0.019f, 0);
            myTrigger.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Transform intBut = transform.Find("Interact Button");
            intBut.localScale = new Vector3(-intBut.localScale.x, intBut.localScale.y, intBut.localScale.z);
        }
        mySound.clip = Resources.Load<AudioClip>("Audio/doorClosed");
        mySound.Play();
        myTrigger.Disable();
        hitbox.enabled = true;
        opened = false;
        GetComponent<InteractorInfo>().used = use;
    }

    void Awake()
    {
        hitbox = transform.Find("Hitbox").gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mySound = GetComponent<AudioSource>();
        myTrigger = transform.Find("Map Trigger").gameObject.GetComponent<MapTrigger>();
    }
}
