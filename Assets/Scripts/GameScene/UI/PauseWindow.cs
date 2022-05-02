using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private PauseButton pauseButton;

    Animator animator = null;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", 0);
    }

    public void Pause()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", 1);
        pauseButton.Pause();
        stageManager.PauseGame();
    }

    public void Play()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        stageManager.PauseGame();
        pauseButton.Play();
    }

    public void Replay()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        stageManager.Restart();
    }

    public void Home()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        GameManager.ResetItem();
        Time.timeScale = 1f;
        GameManager.Load("TitleScene");
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
