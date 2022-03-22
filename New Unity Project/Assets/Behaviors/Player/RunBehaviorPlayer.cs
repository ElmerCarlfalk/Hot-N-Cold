using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviorPlayer : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.speed == 0)
        {
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Run", false);
    }
}
