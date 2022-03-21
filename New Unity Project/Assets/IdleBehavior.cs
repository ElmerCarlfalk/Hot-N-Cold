using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    private float randomAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        randomAttack = Random.Range(0, 3);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            if (randomAttack == 0)
            {
                animator.SetTrigger("SummonAttack");
            }
            else if (randomAttack == 1)
            {
                animator.SetTrigger("SlamAttack");
            }
            else if (randomAttack == 2)
            {
                animator.SetTrigger("RainAttack");
            }
            else if (randomAttack == 3)
            {
                animator.SetTrigger("SpikeAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
