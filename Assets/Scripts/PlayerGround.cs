using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    private bool isplayerGround = false;
    int a;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision vector 비교
        if (collision.gameObject.tag == "Ground")
        {
            isplayerGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isplayerGround = false;
        }
    }

    public bool getGround()
    {
        return (isplayerGround);
    }
}
