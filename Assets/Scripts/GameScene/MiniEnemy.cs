using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject minimap;
    private RectTransform pos;

    private float map_size = 500;
    private float minimapenemystart = -700;
    private float enemystart = -50;
    private float minimapsize = 1300;
    void Start()
    {
        pos = GetComponent<RectTransform>();
        pos.anchoredPosition = new Vector2(minimapenemystart, 33);
    }

    void Update()
    {
        pos.anchoredPosition = new Vector2(minimapenemystart + (enemy.GetComponent<Transform>().position.x - enemystart)/ map_size * minimapsize, 33);
    }
}
