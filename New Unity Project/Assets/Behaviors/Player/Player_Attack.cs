using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AnimationPlayTime", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("AnimationPlayTime") <= 0)
        {
            if (!animator.GetBool("Up") || !animator.GetBool("Down"))
            {
                if (animator.GetFloat("Speed") == 0)
                {
                    animator.SetBool("Idle", true);
                }
                else
                {
                    animator.SetBool("Run", true);
                }
            }
        }
        else
        {
            animator.SetFloat("AnimationPlayTime", animator.GetFloat("AnimationPlayTime") - Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AttackHorizontal", false);
        animator.SetBool("AttackUp", false);
        animator.SetBool("Land", false);
    }
}
