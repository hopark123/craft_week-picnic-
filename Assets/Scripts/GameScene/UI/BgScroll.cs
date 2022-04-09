using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    public RectTransform canvas;
    public Transform goal;
    public Transform playerTrans;
    public Transform respawnPoint;

    Player player;
    float playerSpeed;

    float canvasSize;
    float bgSize;

    float moveLength;
    float mapLength;

    SpriteRenderer sr;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = playerTrans.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bgSize = sr.sprite.rect.width;
        canvasSize = canvas.rect.width;
        moveLength = (bgSize  - canvasSize);
        mapLength = goal.position.x - respawnPoint.position.x;

        playerSpeed = player.moveSpeed;
        transform.localPosition = new Vector3(moveLength / 2, 0, 10);
        Debug.Log("bgSize " + bgSize);
        Debug.Log("canvasSize " + canvasSize);
        Debug.Log("tranfroem " + transform.localPosition);
        Debug.Log("mapLengt" + mapLength);
        Debug.Log("player" + player.transform.position.x);
	    Debug.Log("respawn" + respawnPoint.position.x);
        Debug.Log("moveLen" + moveLength);
        Debug.Log("====================");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log("bgSize " + bgSize);
        Debug.Log("canvasSize " + canvasSize);
        Debug.Log("tranfroem " + transform.localPosition);
        Debug.Log("mapLengt" + mapLength);
        Debug.Log("player" + player.transform.position.x);
        Debug.Log("respawn" + respawnPoint.position.x);
        Debug.Log("moveLen" + moveLength);

        //Debug.Log(transform.localPosition);
        //transform.position = new Vector3(moveLength / canvasSize - (moveLength * (player.transform.position.x - respawnPoint.position.x)) / mapLength , 0, 0);
        transform.localPosition = new Vector3(moveLength / 2 + (-moveLength * (player.transform.position.x - respawnPoint.position.x)) / mapLength, transform.localPosition.y, 10);
        Debug.Log(transform);
    }
}
