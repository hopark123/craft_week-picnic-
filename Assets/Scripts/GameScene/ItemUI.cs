using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public GameObject iconPrefab;
    List<GameObject> icons;

    void Awake()
    {
        icons = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Add(uint itemCode)
    {
    }
}
