using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    Material mat = null;
    Vector2 offset;
    [SerializeField]
    private float velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = new Vector2(velocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (mat != null)
            mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
