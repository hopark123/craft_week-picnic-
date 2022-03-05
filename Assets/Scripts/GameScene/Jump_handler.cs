using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;
    [SerializeField] private float jumppower;
    private bool isTouch = false;
    private Rigidbody2D rigid;
    private Vector3 jumpNext;
    private Animator animator;

    void Start()
    {
        rigid = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
    }
    
    void FixedUpdate() //Fixed Update : 물리엔진2
    {

        if (isTouch) {
            TryJump();
        }
        //Debug.Log("doubjump" + doubleJump + "ground" + player.GetComponent<PlayerGround>().getGround());
        isTouch = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        jumpNext = new Vector3(0, jumppower, 0);
        //Debug.Log("up");
        isTouch = true;
    }

    public void TryJump()
    {
        if (player.GetComponent<PlayerJump>().getJumpcnt() < 2)
        {
            Vector2 temp = rigid.velocity;
            temp.y = 0;
            rigid.velocity = temp;
            rigid.AddForce(jumpNext, ForceMode2D.Impulse);
            player.GetComponent<PlayerJump>().playerisjump();
            animator.SetBool("jump", true);
        }

    }
}
