using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        animator.SetBool("jump", true);
    }

    public void JumpEnd()
    {
        animator.SetBool("jump", false);
    }

    public void Slide()
    {
        animator.SetBool("slide", true);
    }

    public void SlideEnd()
    {
        animator.SetBool("slide", false);
    }
    
    public void Hit()
    {
        animator.SetBool("hit", true);
    }
    
    public void HitEnd()
    {
        animator.SetBool("hit", false);
    }
}
