using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide_handler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject player;

    void Update()
    {
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        player.GetComponent<BoxCollider2D>().offset = new Vector2(0, (float)-0.5);
        player.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        player.GetComponent<BoxCollider2D>().size = new Vector2(1, 2);
    }

}
