using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public StageManager stageManager;
    public Sprite pauseImg;

    Image buttonImg;
    Button button;

    void Awake()
    {
        buttonImg = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        buttonImg.sprite = pauseImg;
    }

    public void Pause()
    {
        buttonImg.enabled = false;
        button.enabled = false;
    }

    public void Play()
    {
        buttonImg.enabled = true;
        button.enabled = true;
        buttonImg.sprite = pauseImg;
    }
}
