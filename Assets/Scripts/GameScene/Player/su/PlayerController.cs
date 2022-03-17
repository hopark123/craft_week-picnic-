using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button jumpButton;
    public Button slideButton;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        jumpButton.onClick.AddListener(player.Jump);

        //add eventdata to slideButton
        EventTrigger trigger = slideButton.gameObject.AddComponent<EventTrigger>();

        //add pointerDown event to trigger
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => player.Slide());
        trigger.triggers.Add(pointerDown);

        //add pointerUp event to trigger
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((eventData) => player.Stand());
        trigger.triggers.Add(pointerUp);
    }
}
