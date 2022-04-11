using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AnimationPlayTime", animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("HaveDied", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("AnimationPlayTime") <= 0)
        {
            //animator.SetBool("Die", false);
        }
        else
        {
            animator.SetFloat("AnimationPlayTime", animator.GetFloat("AnimationPlayTime") - Time.deltaTime);
        }
    }
}
