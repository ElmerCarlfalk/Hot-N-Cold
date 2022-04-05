using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanManager : MonoBehaviour
{
    int chooseAttackType;

    public float minIdleTime;
    public float maxIdleTime;
    float IdleTime;

    public float minAttackDuration;
    public float maxAttackDuration;
    float AttackDuration;

    public float summonCD;
    float summonCDTimer;

    public float spikeAttackCD;
    float spikeAttackCDTimer;

    public float slamAttackCD;
    float slamAttackCDTimer;

    public float rainAttackCD;
    float rainAttackCDTimer;

    public Transform[] summonPos;

    public GameObject summonProjectile;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        IdleTime = Random.Range(minIdleTime, maxIdleTime);
        chooseAttackType = Random.Range(0, 0);
        AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
        summonCDTimer = summonCD;
    }

    void FixedUpdate()
    {
        if (IdleTime <= 0)
        {
            if (chooseAttackType == 0)
            {
                animator.SetBool("SummonAttack", true);
                SummonAttack();
            }
            else if (chooseAttackType == 1)
            {
                animator.SetBool("SpikeAttack", true);
                SpikeAttack();
            }
            else if (chooseAttackType == 2)
            {
                animator.SetBool("SlamAttack", true);
                SlamAttack();
            }
            else
            {
                animator.SetBool("RainAttack", true);
                RainAttack();
            }
        }
        else
        {
            IdleTime -= Time.fixedDeltaTime;
        }

    }

    void SummonAttack()
    {
        if (AttackDuration <= 0)
        {
            animator.SetBool("Idle", true);
            IdleTime = Random.Range(minIdleTime, maxIdleTime);
            chooseAttackType = Random.Range(0, 3);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            summonCDTimer = summonCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (summonCDTimer <= 0)
        {
            Vector2 summonPosition = summonPos[Random.Range(0, summonPos.Length - 1)].position;
            Instantiate(summonProjectile, summonPosition, Quaternion.identity);
            summonCDTimer = summonCD;
        }
        else
        {
            summonCDTimer -= Time.fixedDeltaTime;
        }
    }

    void SpikeAttack()
    {
        if (AttackDuration <= 0)
        {
            animator.SetBool("Idle", true);
            IdleTime = Random.Range(minIdleTime, maxIdleTime);
            chooseAttackType = Random.Range(0, 3);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            spikeAttackCDTimer = spikeAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (spikeAttackCDTimer <= 0)
        {
            //Attack here
            spikeAttackCDTimer = spikeAttackCD;
        }
        else
        {
            spikeAttackCDTimer -= Time.fixedDeltaTime;
        }
    }

    void SlamAttack()
    {
        if (AttackDuration <= 0)
        {
            animator.SetBool("Idle", true);
            IdleTime = Random.Range(minIdleTime, maxIdleTime);
            chooseAttackType = Random.Range(0, 3);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            slamAttackCDTimer = slamAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (slamAttackCDTimer <= 0)
        {
            //Attack here
            slamAttackCDTimer = slamAttackCD;
        }
        else
        {
            slamAttackCDTimer -= Time.fixedDeltaTime;
        }
    }

    void RainAttack()
    {
        if (AttackDuration <= 0)
        {
            animator.SetBool("Idle", true);
            IdleTime = Random.Range(minIdleTime, maxIdleTime);
            chooseAttackType = Random.Range(0, 3);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            rainAttackCDTimer = rainAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (rainAttackCDTimer <= 0)
        {
            //Attack here
            rainAttackCDTimer = rainAttackCD;
        }
        else
        {
            rainAttackCDTimer -= Time.fixedDeltaTime;
        }
    }
}
