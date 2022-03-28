using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateMachineBehaviour
{
    public float attackTime;
    private float attackTimeCounter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimeCounter = attackTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackTimeCounter <= 0)
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
        else
        {
            attackTimeCounter -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("AttackHorizontal", false);
        animator.SetBool("AttackUp", false);
        attackTimeCounter = attackTime;
    }
}
