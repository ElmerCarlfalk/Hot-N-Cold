using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AirJump : StateMachineBehaviour
{


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AirJump", false);
    }
}
