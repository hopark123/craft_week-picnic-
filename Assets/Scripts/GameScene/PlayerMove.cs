using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    Vector3 moveNext;
    
    void Start()
    {
        moveNext = new Vector3(moveSpeed, 0, 0);
    }
    
    void FixedUpdate()
    {
        this.transform.Translate(moveNext * Time.deltaTime);
    }
}
