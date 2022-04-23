using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public StageManager stageManager;
    public PauseButton pauseButton;

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
        animator.SetBool("pause", false);
    }

    public void Pause()
    {
        animator.SetBool("pause", true);
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
    }

    public void Play()
    {
        animator.SetBool("pause", false);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, playAction);
    }

    public void Replay()
    {
        animator.SetBool("pause", false);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, replayAction);
    }

    public void Home()
    {
        animator.SetBool("pause", false);
        state.ExitAnimation(animator, animator.GetCurrentAnimatorStateInfo(0), 0, homeAction);
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
