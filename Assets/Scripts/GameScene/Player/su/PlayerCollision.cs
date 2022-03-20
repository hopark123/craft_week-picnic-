using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Player player;
    
    private ContactPoint2D contact;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            player.Hit();
            return;
        }

        contact = collision.contacts[0];
        float projection = Vector2.Dot(Vector2.down, contact.normal);

        if (projection <= -0.3f && projection >= -1)// 땅의 윗부분에 충돌했는지
        {
            //init jump
            player.IsGround = true;
            player.Jumpcnt = 0;
            player.Animate().SetBool("jump", false);
        }
        else
        {
            player.Hit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (player.IsGround)
            player.IsGround = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(contact.point, contact.point + contact.normal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
            player.GetItem(collision.gameObject);
        if (collision.CompareTag("Goal"))
            player.Goal();
    }
}
