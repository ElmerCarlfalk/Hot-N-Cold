using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    public float moveSpeed;
    public float attackSpeed;

    public float veldamp;
    public float stifness;
    public float damp;

    public float attackRange;

    public float attackTime;
    private float attackTimeCounter;
    private bool isAttacking = false;

    public float attackCD;
    private float attackCDCounter;
    private bool attackOnCD = false;

    public float stunnedTime;
    private float stunnedTimeCounter;
    public bool isStunned = false;

    [HideInInspector]
    public Vector2 knockbackDir;

    public float knockback;

    private bool activateOnStun = true;

    Vector3 currentVel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        stunnedTimeCounter = stunnedTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // procedure TEntityPOI.Spring(const AGotoCoAPos:TPointF; stifness, damp, veldamp: Single);
        //begin 
        //F = kx - bv where k=stifness, x=dist to point, b=damp, v=velocity
        //k=stifness, x=dist to point, b=damp, v=velocity
        //newv = v + (kx - bv) * deltatime where k=stifness, x=dist to point, b=damp, v=current velocity
        if (!isStunned)
        {
            if (!isAttacking)
            {
                Vector2 dir = (player.transform.position - transform.position);
                float x = dir.magnitude;
                dir.Normalize();
                float v = Vector2.Dot(rb.velocity, dir);
                rb.velocity = rb.velocity * (1 - veldamp * Time.fixedDeltaTime) + dir * (stifness * x - damp * v) * Time.fixedDeltaTime;

                if (rb.velocity.magnitude > moveSpeed)
                {
                    Vector3 vel = rb.velocity;
                    vel.Normalize();
                    rb.velocity = vel * moveSpeed;
                }
            }

            if (!isAttacking)
            {
                if (!attackOnCD)
                {
                    float distanceToPlayer = (player.transform.position - transform.position).magnitude;
                    if (distanceToPlayer < attackRange)
                    {
                        Vector3 attackDir = (player.transform.position - transform.position).normalized;
                        //Vector3 attackDir = rb.velocity;
                        attackDir.Normalize();
                        rb.velocity = attackDir * attackSpeed;
                        attackTimeCounter = attackTime;
                        isAttacking = true;
                    }
                }
                else
                {
                    if (attackCDCounter <= 0)
                    {
                        attackOnCD = false;
                    }
                    else
                    {
                        attackCDCounter -= Time.fixedDeltaTime;
                    }
                }
            }
            else
            {
                if (attackTimeCounter <= 0)
                {
                    attackCDCounter = attackCD;
                    attackOnCD = true;
                    isAttacking = false;
                }
                else
                {
                    attackTimeCounter -= Time.fixedDeltaTime;
                }
            }
        }
        else //If stunned true
        {
            if (activateOnStun)
            {
                if (stunnedTimeCounter <= 0)
                {
                    stunnedTimeCounter = stunnedTime;
                    activateOnStun = true;
                    rb.velocity = currentVel;
                    isStunned = false;
                }
                else
                {
                    stunnedTimeCounter -= Time.fixedDeltaTime;
                }
            }
            else //This activates when first stunned
            {
                currentVel = rb.velocity;
                rb.velocity = Vector3.zero;

                if (rb.velocity.y != 0)
                {
                    Vector2 currentVel = rb.velocity;
                    currentVel.y = knockbackDir.y * knockback;
                    rb.velocity = currentVel;
                }
                else
                {
                    Vector2 currentVel = rb.velocity;
                    currentVel.x = knockbackDir.x * knockback;
                    rb.velocity = currentVel;
                }

                activateOnStun = false;
            }
        }
    }
}