using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public Button jumpButton;
    public Button slideButton;

    private Player player;
    private Rigidbody2D rd;
    private BoxCollider2D col;

    private Vector2 standSize;
    private Vector2 slideSize;
    private Vector2 standOffset;
    private Vector2 slideOffset;

    private bool jump = false;

    void Awake()
    {
        player = GetComponent<Player>();
        rd = player.GetComponent<Rigidbody2D>();
        col = player.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
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
        slideSize = new Vector2(standSize.x, standSize.y / 2);
        slideOffset = new Vector2(standOffset.x, standOffset.y - col.size.y / 4);
    }

    private void Update()
    {
        if (player.IsAlive)
            Move();
    }

    private void FixedUpdate()
    {
        if (jump && player.IsAlive)
        {
            JumpAction();
            jump = false;
        }
    }

    private void Move()
    {
        player.transform.Translate(Vector3.right * player.moveSpeed * Time.deltaTime);
    }

    private void Jump() => jump = true;

    private void JumpAction()
    {
        if (player.Jumpcnt < player.MaxJumpCnt)
        {
            rd.velocity = new Vector2(rd.velocity.x, 0);
            rd.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);

            player.Jumpcnt++;
            player.Animate().SetBool("jump", true);
        }
    }

    private void Slide()
    {
        col.offset = slideOffset;
        col.size = slideSize;
        player.Animate().SetBool("slide", true);
    }

    private void Stand()
    {
        col.offset = standOffset;
        col.size = standSize;
        player.Animate().SetBool("slide", false);
    }
}
