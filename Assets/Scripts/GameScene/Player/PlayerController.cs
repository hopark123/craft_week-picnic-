using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Button jumpButton;
    [SerializeField]
    private Button slideButton;

    private Player player = null;
    private Rigidbody2D rd = null;
    private BoxCollider2D col = null;

    private Vector2 standSize;
    private Vector2 slideSize;
    private Vector2 standOffset;
    private Vector2 slideOffset;

    private bool jump = false;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        if (player != null)
        {
            rd = player.GetComponent<Rigidbody2D>();
            col = player.GetComponent<BoxCollider2D>();
        }

        //register controller button
        //jumpButton.onClick.AddListener(Jump);
        EventTrigger jmpTrigger = jumpButton.gameObject.AddComponent<EventTrigger>();
        //add pointerDown event to jmpTrigger
        var jmpPointerDown = new EventTrigger.Entry();
        jmpPointerDown.eventID = EventTriggerType.PointerDown;
        jmpPointerDown.callback.AddListener((eventData) => Jump());
        jmpTrigger.triggers.Add(jmpPointerDown);

        //add eventdata to slideButton
        EventTrigger trigger = slideButton.gameObject.AddComponent<EventTrigger>();

        //add pointerDown event to trigger
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => Slide());
        trigger.triggers.Add(pointerDown);

        //add pointerUp event to trigger
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((eventData) => Stand());
        trigger.triggers.Add(pointerUp);

        //init collidersize
        standSize = col.size;
        standOffset = col.offset;
        slideSize = new Vector2(standSize.x, standSize.y / 3);
        slideOffset = new Vector2(standOffset.x, standOffset.y - col.size.y / 4);
    }

    private void Update()
    {
        if (player != null && player.IsAlive)
            Move();
    }

    private void FixedUpdate()
    {
        if (jump && player != null && player.IsAlive)
        {
            JumpAction();
            jump = false;
        }
    }

    private void Move()
    {
        if (player != null)
            player.transform.Translate(Vector3.right * player.moveSpeed * Time.deltaTime);
    }

    private void Jump() => jump = true;

    private void JumpAction()
    {
        if (player != null && rd != null)
        {
            if (player.Jumpcnt < player.MaxJumpCnt)
            {
                rd.velocity = new Vector2(rd.velocity.x, 0);
                rd.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);

                player.Jumpcnt++;
                player.Jump();
            }
        }
    }

    private void Slide()
    {
        if (col != null && player != null)
        {
            col.offset = slideOffset;
            col.size = slideSize;
            player.Slide(); ;
        }
    }

    private void Stand()
    {
        if (col != null && player != null)
        {
            col.offset = standOffset;
            col.size = standSize;
            player.Stand();
        }
    }
}
