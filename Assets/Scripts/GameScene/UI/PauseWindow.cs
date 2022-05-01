using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private PauseButton pauseButton;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("pauseStatus", 0);
    }

    public void Pause()
    {
        animator.SetInteger("pauseStatus", 1);
        pauseButton.Pause();
        stageManager.PauseGame();
    }

    private void playAction()
    {
        stageManager.PauseGame();
        pauseButton.Play();
    }

    private void replayAction()
    {
        stageManager.Restart();
    }

    private void homeAction()
    {
        GameManager.ResetItem();
        Time.timeScale = 1f;
        GameManager.Load("TitleScene");
    }

    public void Play()
    {
        animator.SetInteger("pauseStatus", -1);
        playAction();
    }

    public void Replay()
    {
        animator.SetInteger("pauseStatus", -1);
        replayAction();
    }

    public void Home()
    {
        animator.SetInteger("pauseStatus", -1);
        homeAction();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!stageManager.Pause)
                Pause();
        }
    }
}
