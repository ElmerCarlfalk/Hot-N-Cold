using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Land : StateMachineBehaviour
{


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.speed != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Idle", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Land", false);
    }
}
