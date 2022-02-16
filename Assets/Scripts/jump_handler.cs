using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jump_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;
    [SerializeField] private float jumpspeed;
    private bool isTouch = false;
    private bool isJump = false;
    private bool isFall = false;
    private Vector3 jumpNext;
    private Rigidbody rigid;

    void Start()
    {
        rigid = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isTouch) {
            TryJump();
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        jumpNext = new Vector3(0, 0, jumpspeed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = true;
        
    }

    public void TryJump()
    {
        if (isJump)
        {
            isJump = false;
            isFall = true;
        }
        else if (!isJump && isTouch)
        {
            isJump = true;
            rigid.AddForce(jumpNext);
        }
    }

}
