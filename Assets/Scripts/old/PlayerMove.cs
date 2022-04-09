using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    Vector3 moveNext;
    Rigidbody2D rd;
    
    void Start()
    {
        moveNext = new Vector3(moveSpeed, 0, 0);
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.transform.Translate(moveNext * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //this.transform.Translate(moveNext * Time.deltaTime);
        //rd.AddForce(Vector2.right * moveNext);
    }
}
