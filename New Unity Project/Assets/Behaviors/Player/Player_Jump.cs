using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : StateMachineBehaviour
{


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("Up") || !animator.GetBool("Down"))
        {
            if (animator.speed == 0)
            {
                animator.SetBool("Idle", true);
            }
            else
            {
                animator.SetBool("Run", true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Jump", false);
    }
}