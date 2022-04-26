using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    Material mat;
    Vector2 offset;
    
    [SerializeField]
    private float velocity;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Start()
    {
        offset = new Vector2(velocity, 0);
    }

    void LateUpdate()
    {
        mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
