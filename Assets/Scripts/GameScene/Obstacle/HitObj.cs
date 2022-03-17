using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObj : MonoBehaviour
{
    public  GameManager gameManager;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
            return;
        //if (collision.transform.GetComponent<Rigidbody2D>().velocity.y < 0)
        //{
        //    Debug.Log("hit");
        //    return;
        //}
        //else
        //{
        //    gameManager.Kill();
        //}
        //gameManager.Kill();
    }
}
