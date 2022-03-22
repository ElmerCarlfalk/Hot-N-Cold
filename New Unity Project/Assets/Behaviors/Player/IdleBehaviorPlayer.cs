using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviorPlayer : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.speed != 0)
        {
            animator.SetTrigger("Run");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Idle", false);
    }
}