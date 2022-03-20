using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;
    private BoxCollider2D coll = null;
    //private CapsuleCollider2D coll = null;
    private Vector2 standSize;
    private Vector2 standOffset;
    private Vector2 slideSize;
    private Vector2 slideOffset;

    Animator animator;

    private void Start()
    {
        coll = player.GetComponent<BoxCollider2D>();
        //coll = player.GetComponent<CapsuleCollider2D>();
        animator = player.GetComponent<Animator>();
        if (coll)
        {
            standOffset = coll.offset;
            standSize = coll.size;
            slideSize = new Vector2(coll.size.x, coll.size.y / 2);
            slideOffset = new Vector2(coll.offset.x, coll.offset.y - coll.size.y / 4);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        coll.offset = slideOffset;
        coll.size = slideSize;
        animator.SetBool("slide", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        coll.offset = standOffset;
        coll.size = standSize;
        animator.SetBool("slide", false);
    }

}