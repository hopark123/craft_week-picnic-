using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public StageManager stageManager;
    public Sprite pauseImg;

    Image buttonImg = null;
    Button button = null;

    void Awake()
    {
        buttonImg = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (buttonImg != null)
            buttonImg.sprite = pauseImg;
    }

    public void Pause()
    {
        if (buttonImg != null)
            buttonImg.enabled = false;
        if (button != null)
            button.enabled = false;
    }

    public void Play()
    {
        if (buttonImg != null)
        {
            buttonImg.enabled = true;
            buttonImg.sprite = pauseImg;
        }
        if (button != null)
            button.enabled = true;
    }
}
