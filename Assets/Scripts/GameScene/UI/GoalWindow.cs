using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalWindow : MonoBehaviour
{
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private PauseButton pauseButton;
    
    AnimationState state;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        state = animator.GetBehaviour<AnimationState>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("pauseStatus", 0);
    }

    public void Goal()
    {
        animator.SetInteger("pauseStatus", 1);
        pauseButton.Pause();
    }

    private void nextStageAction()
    {
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

    private void replayAction()
    {
        stageManager.Restart();
    }

    private void homeAction()
    {
        Time.timeScale = 1f;
        if (GameManager.stageNumber < 2)
            GameManager.stageNumber++;
        else
            GameManager.stageNumber = 0;
        GameManager.Load("TitleScene");
    }

    public void nextStage()
    {
        animator.SetInteger("pauseStatus", -1);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, nextStageAction);
    }

    public void Replay()
    {
        animator.SetInteger("pauseStatus", -1);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, replayAction);
    }

    public void Home()
    {
        animator.SetInteger("pauseStatus", -1);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, homeAction);
    }
}
