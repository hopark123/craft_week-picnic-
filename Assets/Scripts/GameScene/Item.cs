using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field:SerializeField]
    public string Id { get; private set; }

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.name = Id;
        sr.sprite = Resources.Load<Sprite>("items/" + Id) as Sprite;
    }
}