using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalWindow : MonoBehaviour
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

    public void Goal()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", 1);
        pauseButton.Pause();
    }

    public void Replay()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        stageManager.Restart();
    }

    public void nextStage()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        Time.timeScale = 1.0f;
        if (GameManager.stageNumber < 2)
        {
            GameManager.stageNumber++;
            GameManager.Load("GameScene" + (GameManager.stageNumber + 1).ToString());
            return;
        }
        GameManager.stageNumber = 0;
        GameManager.Load("EndingScene");
    }

    public void Home()
    {
        if (animator != null)
            animator.SetInteger("pauseStatus", -1);
        Time.timeScale = 1f;
        if (GameManager.stageNumber < 2)
            GameManager.stageNumber++;
        else
            GameManager.stageNumber = 0;
        GameManager.Load("TitleScene");
    }
}
