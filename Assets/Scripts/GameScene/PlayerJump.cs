using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool isGround = false;
    private int jumpcnt = 0;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // check player is ground //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision vector 비교
        if (collision.gameObject.tag == "Ground")
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
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public bool getGround()
    {
        return (isGround);
    }

    public void playerisjump()
    {
        ++jumpcnt;
    }
    public int getJumpcnt()
    {
        return (jumpcnt);
    }

}
