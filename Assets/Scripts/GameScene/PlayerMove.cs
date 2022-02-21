using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") / 10 ;
        //if(Input.GetKeyDown(KeyCode.D))
        //    rigid.AddForce(Vector2.right * speed);
        //if (Input.GetKeyDown(KeyCode.A))
        //    rigid.AddForce(-Vector2.right * speed);


        //rigid.AddForce(Vector2.right * h);

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    }
}
