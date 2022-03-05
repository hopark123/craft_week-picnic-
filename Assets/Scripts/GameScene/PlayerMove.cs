using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    Rigidbody2D rigid;
    Vector3 moveNext;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveNext = new Vector3(moveSpeed, 0, 0);
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

    void FixedUpdate()
    {
        this.transform.Translate(moveNext * Time.deltaTime);
    }
}
