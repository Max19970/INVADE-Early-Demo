using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    private Animator animator;
    private AudioSource movementSound;
    private AudioSource interactionSound;
    private PlayerController controller;

    private bool jump;

    public void PlayLongEyesAnim()
    {
        animator.SetFloat("speed", 1);
    }

    public void StopLongEyesAnim()
    {
        animator.SetFloat("speed", 0);
    }

    public void PlayFallAnim()
    {
        animator.SetInteger("moveV", -1);
        animator.Play("Fall 2");
    }

    public void PlayInflectionAnim()
    {
        StartCoroutine(Inflection());
    }

    private IEnumerator Inflection()
    {
        yield return new WaitForSeconds((GetComponent<PlayerController>().maxJumpHeight * 10f) - (39 / 60f));
        animator.Play("Inflection");
    }

    public void PlayJumpSound()
    {
        movementSound.clip = Resources.Load<AudioClip>("Audio/playerJump");
        movementSound.Play();
    }

    public void PlayFallSound()
    {
        movementSound.clip = Resources.Load<AudioClip>("Audio/playerFall");
        movementSound.Play();
    }

    public void PlayWalkSound()
    {
        movementSound.clip = Resources.Load<AudioClip>("Audio/move" + Random.Range(1, 5));
        movementSound.Play();
    }

    public void AnimateMove()
    {
        animator.SetBool("sliding", false);
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0) {animator.SetInteger("moveH", 1); transform.localScale = new Vector3(1, 1, 1);}
        else if (moveHorizontal < 0) {animator.SetInteger("moveH", -1); transform.localScale = new Vector3(-1, 1, 1);}
        else animator.SetInteger("moveH", 0);

        if (!jump)
        {
            if (controller.myRigidBody.velocity.y > 0) animator.SetInteger("moveV", 1);
            else if (controller.myRigidBody.velocity.y < 0) animator.SetInteger("moveV", -1);
            else animator.SetInteger("moveV", 0);
        }
        else
        {
            if (controller.myRigidBody.velocity.y < 0) {animator.SetInteger("moveV", -1); jump = false;}
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && controller.jumpQuantity > 0) {jump = true; animator.SetInteger("moveV", 1);}
    }

    public void AnimateSlide()
    {
        animator.SetBool("sliding", true);
        bool toRight = transform.localScale.x == 1;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || (toRight ? Input.GetKeyDown(KeyCode.RightArrow) : Input.GetKeyDown(KeyCode.LeftArrow))) && controller.jumpQuantity > 0)
        {
            jump = true;
            animator.SetInteger("moveV", 1);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        movementSound = transform.Find("Movement Sound Source").gameObject.GetComponent<AudioSource>();
        interactionSound = transform.Find("Interaction Sound Source").gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!controller.blocked)
        {
            if (!controller.sliding) AnimateMove();
            else AnimateSlide();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            animator.SetInteger("moveV", 0);
        }
        
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            StopAllCoroutines();
            animator.Play("Inflection");
        }
    }
}
