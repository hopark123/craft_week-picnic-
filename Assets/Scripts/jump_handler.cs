using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;
    [SerializeField] private float jumppower;
    private bool isTouch = false;
    private bool isJump = false;
    private int doubleJump = 0;
    private Rigidbody2D rigid;
    private Vector3 jumpNext;

    void Start()
    {
        rigid = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player.GetComponent<PlayerGround>().getGround())
        {
            isJump = false;
            doubleJump = 0;
        }
        if (isTouch) {
            TryJump();
        }
        Debug.Log("doubjump" + doubleJump + "ground" + player.GetComponent<PlayerGround>().getGround());
        isTouch = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        jumpNext = new Vector3(0, jumppower, 0);
        Debug.Log("up");
        isTouch = true;
    }

    public void TryJump()
    {
        if (doubleJump < 2)
        {
            rigid.velocity = Vector3.zero;
            //if (isJump)
            //{
            //    rigid.velocity = Vector3.zero;
            //}
            //player.transform.Translate(jumpNext * Time.deltaTime);
            isJump = true;
            rigid.AddForce(jumpNext, ForceMode2D.Impulse);
            ++doubleJump;
        }

    }
}
/*
    터치 점프 떨어지는중
        o    o   o   무시 
    o    x   o   한번 더 점프 
    o    x   x   점프
        x    o   o  계속떨어
        x    x   o  계속떨어
    x    x   x  아무일x
*/


