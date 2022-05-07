using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public Transform goal;
    public Transform respawnPoint;

    private float mapLength;
    private float pos = 0.0f;

    private Slider slider = null;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mapLength = goal.position.x - respawnPoint.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pos = (player.transform.position.x - respawnPoint.transform.position.x) / mapLength;
        if (slider != null)
            slider.value = pos;
    }
}
