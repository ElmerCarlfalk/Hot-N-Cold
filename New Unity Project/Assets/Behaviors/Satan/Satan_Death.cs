using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satan_Death : StateMachineBehaviour
{
    float animationPlayTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animationPlayTime = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animationPlayTime <= 0)
        {
            animator.SetBool("Death", false);
        }
        else
        {
            animationPlayTime -= Time.fixedDeltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
