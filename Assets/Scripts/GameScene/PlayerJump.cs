using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public int jumpcnt { get; private set; } = 0;
    public bool isGround { get; private set; }
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // check player is ground //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) //ground
        {
            animator.SetBool("jump", false);
            if (this.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                isGround = true;
                jumpcnt = 0;
            }   
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) //ground
        {
            isGround = false;
        }
    }
    
    public void playerisjump()
    {
        ++jumpcnt;
        animator.SetBool("jump", true);
    }
}
