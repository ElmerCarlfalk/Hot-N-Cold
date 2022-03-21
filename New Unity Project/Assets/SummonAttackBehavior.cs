using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAttackBehavior : StateMachineBehaviour
{
    public Transform[] summonPos;
    public GameObject projectile;
    public int summonAmountMin;
    public int summonAmountMax;
    private int summonAmount;

    public float summonCD;
    private float summonCDTimer;
    private bool onCD = true;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        summonAmount = Random.Range(summonAmountMin, summonAmountMax);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i > summonAmount; i++)
        {
            while(onCD)
            {
                if(summonCDTimer <= 0)
                {
                    int summonThisPos = Random.Range(0, summonPos.Length - 1);
                    Instantiate(projectile, summonPos[summonThisPos]);
                    onCD = false;
                }
                else
                {
                    summonCDTimer -= Time.deltaTime;
                }
            }
            summonCDTimer = summonCD;
            onCD = true;
        }
        animator.SetTrigger("idle");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
