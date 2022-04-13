using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanManager : Enemy
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
    public Transform[] spikeSummonPos;
    public Transform[] slamStartPos;
    public Transform[] rainSummonPos;

    public GameObject summonProjectile;
    public GameObject spikeObject;
    public GameObject slamProjectile;
    public GameObject[] rainProjectile;

    protected override void Start()
    {
        base.Start();
        IdleTime = Random.Range(minIdleTime, maxIdleTime);
        chooseAttackType = Random.Range(3, 4);
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
            chooseAttackType = Random.Range(0, 4);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            summonCDTimer = summonCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (summonCDTimer <= 0)
        {
            Vector2 summonPosition = summonPos[Random.Range(0, summonPos.Length)].position;
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
            chooseAttackType = Random.Range(0, 4);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            spikeAttackCDTimer = spikeAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (spikeAttackCDTimer <= 0)
        {
            int safePoint = Random.Range(0, spikeSummonPos.Length);
            for(int i = 0; i < spikeSummonPos.Length; i++)
            {
                if(i != safePoint)
                {
                    Instantiate(spikeObject, spikeSummonPos[i].position, Quaternion.identity);
                }
            }
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
            chooseAttackType = Random.Range(0, 4);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            slamAttackCDTimer = slamAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (slamAttackCDTimer <= 0)
        {
            Vector2 summonPosition = slamStartPos[Random.Range(0, slamStartPos.Length)].position;
            Instantiate(slamProjectile, summonPosition, Quaternion.identity);
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
            chooseAttackType = Random.Range(0, 4);
            AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
            rainAttackCDTimer = rainAttackCD;
        }
        else
        {
            AttackDuration -= Time.fixedDeltaTime;
        }

        if (rainAttackCDTimer <= 0)
        {
            Vector2 summonPosition = rainSummonPos[Random.Range(0, rainSummonPos.Length)].position;
            Instantiate(rainProjectile[Random.Range(0, rainProjectile.Length)], summonPosition, Quaternion.identity);
            rainAttackCDTimer = rainAttackCD;
        }
        else
        {
            rainAttackCDTimer -= Time.fixedDeltaTime;
        }
    }
}
