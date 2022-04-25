using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanManager : Enemy
{
    int chooseAttackType;

    [Header("Attack Cooldowns")]
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
    public float timeUntilRainAttack;
    float timeUntilRainAttackCounter;
    bool startedRain = false;
    float timeUntilStart;
    bool hasStarted = false;
    float firstDeathParticleDuration;
    public float timeUntilSecondDeathParticle;
    bool isDead = false;


    [Header("Screen Shake")]
    public float shakeIntensityRainStart;
    //public float shakeDurationRainStart;

    public float shakeIntensityEnter;

    public float shakeIntensityDeathStart;
    public float shakeIntensityDeathEnd;
    public float shakeDurationDeathEnd;

    [Header("Attack Summon Positions")]
    public Transform[] summonPos;
    public Transform[] spikeSummonPos;
    public Transform[] slamStartPos;
    public Transform[] rainSummonPos;

    [Header("Attack Projectiles")]
    public GameObject summonProjectile;
    public GameObject spikeObject;
    public GameObject slamProjectile;
    public GameObject[] rainProjectile;

    [Header("Particle Effects")]
    public GameObject idleEffect;
    public GameObject[] deathEffects;

    protected override void Start()
    {
        base.Start();
        IdleTime = Random.Range(minIdleTime, maxIdleTime);
        chooseAttackType = Random.Range(0, 4);
        AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
        summonCDTimer = summonCD;
        timeUntilRainAttackCounter = timeUntilRainAttack;
        firstDeathParticleDuration = deathEffects[0].GetComponent<ParticleSystem>().main.duration;
        timeUntilStart = spawnParticles.GetComponent<ParticleSystem>().main.duration;
        CinemachineShake.Instance.ShakeCamera(shakeIntensityEnter, timeUntilStart);
        PlayerMovement.Instance.Stun(timeUntilStart);
    }

    void Update()
    {
        if (hasStarted)
        {
            if (!isDead)
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
            else
            {
                Die();
            }
        }
        else
        {
            if(timeUntilStart <= 0)
            {
                Instantiate(idleEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, 0));
                hasStarted = true;
            }
            else
            {
                timeUntilStart -= Time.deltaTime;
            }
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
        if (!startedRain)
        {
            CinemachineShake.Instance.ShakeCamera(shakeIntensityRainStart, timeUntilRainAttack + 0.5f);
            startedRain = true;
        }

        if (timeUntilRainAttackCounter <= 0)
        {
            if (AttackDuration <= 0)
            {
                animator.SetBool("Idle", true);
                IdleTime = Random.Range(minIdleTime, maxIdleTime);
                chooseAttackType = Random.Range(0, 4);
                AttackDuration = Random.Range(minAttackDuration, maxAttackDuration);
                startedRain = false;
                timeUntilRainAttackCounter = timeUntilRainAttack;
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
        else
        {
            timeUntilRainAttackCounter -= Time.deltaTime;
        }
    }

    protected override void Die()
    {
        if (!isDead)
        {
            Instantiate(deathEffects[0], new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, -45));
            CinemachineShake.Instance.ShakeCamera(shakeIntensityDeathStart, firstDeathParticleDuration);
            isDead = true;
        }

        if(firstDeathParticleDuration <= 0)
        {
            if(timeUntilSecondDeathParticle <= 0)
            {
                CinemachineShake.Instance.ShakeCamera(shakeIntensityDeathEnd, shakeDurationDeathEnd);
                Instantiate(deathEffects[1], new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, 0));
                FadeScreen.Instance.FadeImage(false, 2, 0);
                Destroy(gameObject);
            }
            else
            {
                timeUntilSecondDeathParticle -= Time.deltaTime;
            }
        }
        else
        {
            firstDeathParticleDuration -= Time.deltaTime;
        }
    }
}
