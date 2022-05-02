using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Player player;

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
        ContactPoint2D contact = collision.contacts[0];
        float projection = Vector2.Dot(Vector2.down, contact.normal);
        if (projection <= -0.3f && projection >= -1)// 땅의 윗부분에 충돌했는지
        {
            Debug.Log("jump init");
            //init jump
            player.IsGround = true;
            player.Jumpcnt = 0;
            player.JumpEnd();
            if (collision.transform.CompareTag("Obs"))
            {
                Obstacle obs = collision.transform.GetComponent<Obstacle>();
                obs.Hit();
                player.Jumpcnt = 2 - obs.jumpAdd;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contact;

        for (int i = 0; i < collision.contactCount; i++)
        {
            contact = collision.contacts[i];
            float projection = Vector2.Dot(Vector2.down, contact.normal);

            if (projection > -0.3f || projection < -1)// 땅의 윗부분에 충돌했는지
                player.Hit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (player.IsGround)
            player.IsGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
            player.GetItem(collision.gameObject);
        if (collision.CompareTag("Goal"))
            player.Goal();
        if (collision.transform.CompareTag("Slow"))
        {
            player.Slow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Slow"))
        {
            player.Slow = false;
        }
    }
}
