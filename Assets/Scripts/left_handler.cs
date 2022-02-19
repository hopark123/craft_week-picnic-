using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Left_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;
    [SerializeField] private float movespeed;

    private Rigidbody rigid;
    private bool isTouch = false;
    private Vector3 moveNext;


    void Update()
    {
        if (isTouch) {
            player.transform.Translate(moveNext * Time.deltaTime);
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
        moveNext = new Vector3(-movespeed, 0, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveNext = Vector3.zero;
        isTouch = false;
    }

}
