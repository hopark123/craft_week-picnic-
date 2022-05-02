using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    Material mat = null;
    Vector2 offset;
    [SerializeField]
    private float velocity;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = new Vector2(velocity, 0);
    }

    void LateUpdate()
    {
        if (mat != null) mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
