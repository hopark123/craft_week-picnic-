using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private Image[] inventory;
    [SerializeField]
    private Sprite disabledImg;
    [SerializeField]
    private Animator pauseAnimator;
    [SerializeField]
    private Animator clearAnimator;
    [SerializeField]
    private GameObject playCanvus;

    public void InitSlot()
    {
        for (int i = 0; i < inventory.Length; i++)
            inventory[i].color = Color.gray;
    }

    public void ActivateSlot(int idx)
    {
        inventory[idx].color = Color.white;
    }

    public void DisableSlot(int idx)
    {
        inventory[idx].sprite = disabledImg;
    }

    public void Pause_Init()
    {
        pauseAnimator.SetInteger("pauseStatus", 0);
    }

    public void Pause_Open()
    {
        playCanvus.SetActive(false);
        pauseAnimator.SetInteger("pauseStatus", 1);
    }

    public void Pause_Close()
    {
        playCanvus.SetActive(true);
        pauseAnimator.SetInteger("pauseStatus", -1);
    }
    
    public void Clear_Init()
    {
        pauseAnimator.SetInteger("pauseStatus", 0);
    }

    public void Clear_Open()
    {
        playCanvus.SetActive(false);
        clearAnimator.SetInteger("pauseStatus", 1);
    }
    
    public void Clear_Close()
    {
        playCanvus.SetActive(true);
        pauseAnimator.SetInteger("pauseStatus", -1);
    }
}
