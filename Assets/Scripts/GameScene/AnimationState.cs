using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : StateMachineBehaviour
{
    public delegate void Func();

    public void ExitAnimation(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Func func)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        func();
    }
}
