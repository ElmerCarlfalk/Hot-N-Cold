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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // procedure TEntityPOI.Spring(const AGotoCoAPos:TPointF; stifness, damp, veldamp: Single);
        //begin 
        //F = kx - bv where k=stifness, x=dist to point, b=damp, v=velocity
        //k=stifness, x=dist to point, b=damp, v=velocity
        //newv = v + (kx - bv) * deltatime where k=stifness, x=dist to point, b=damp, v=current velocity
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
}