using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public StageManager stageManager;
    public Sprite pauseImg;
    public Sprite playImg;

    Image buttonImg;

    void Awake()
    {
        buttonImg = GetComponent<Image>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        buttonImg.sprite = pauseImg;
    }

    public void PressButton()
    {
        stageManager.PauseGame();
        if (stageManager.Pause)
            buttonImg.sprite = playImg;
        else
            buttonImg.sprite = pauseImg;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!stageManager.Pause)
                PressButton();
        }
    }
}
