using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject minimap;
    private RectTransform pos;

    private float map_size = 500;
    private float minimapenemystart = -700;
    private float playerstart = -50;
    private float minimapsize = 1300;

    void Start()
    {
        pos = GetComponent<RectTransform>();
        pos.anchoredPosition = new Vector2(-623, 33);
    }

    void Update()
    {
        pos.anchoredPosition = new Vector2(minimapenemystart + (player.GetComponent<Transform>().position.x - playerstart) / map_size * minimapsize, 33);
    }
}
