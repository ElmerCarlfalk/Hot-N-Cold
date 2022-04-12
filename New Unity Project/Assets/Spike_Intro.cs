using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Intro : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AnimationPlayTime", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("AnimationPlayTime") <= 0)
        {
            animator.SetBool("Shake", true);
        }
        else
        {
            animator.SetFloat("AnimationPlayTime", animator.GetFloat("AnimationPlayTime") - Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
