using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Animator animator;
    //public float value;
    public bool visible;
    
    public bool shown;

    // public void ChangeFullness(float fvalue)
    // {
    //     renderer.sharedMaterial.SetFloat("_Arc2", 360 - fvalue * 360);
    //     value = fvalue;
    // }

    public void Cycle(float ftime)
    {
        if (visible)
        {
            animator.Rebind();
            animator.SetFloat("cycleSpeed", 1 / ftime);
            animator.Play("Indicator Cycle", -1, 0);
        }
    }

    void Update()
    {
        visible = PlayerPrefs.GetInt("enemyDanger") == 1;
        if (visible)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                if (!shown) 
                {
                    shown = true; animator.Play("ShowIndicator", 0, 0);
                }
            }
            else
            {
                if (shown)
                {
                    shown = false; animator.Play("HideIndicator", 0, 0);
                }
            }
        }
        else
        {
            if (shown)
            {
                shown = false; animator.Play("HideIndicator", 0, 0);
            }
        }
    }
}
