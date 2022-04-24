using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : Enemy
{
    public LayerMask whatIsGround;
    public LayerMask playerLayer;
    public float speed;
    public float jumpForce;
    public bool facingRight = false;
    public bool isAttacking = false;
    private float lastAttack = 0;
    public float attackDelay;

    private Rigidbody2D rb;

    public Transform groundCheck;
    public Transform wallCheck;
    public bool isGrounded;
    public bool isOnWall;
    public Transform attackBoxFront;
    bool isPlayerFront;
    public Transform attackBoxUp;
    bool isPlayerUp;
    GameObject player;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (isPlayerFront && !isAttacking)
        {
            Invoke("AttackFront", 0.75f);
            isAttacking = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (isPlayerUp && !isAttacking)
        {
            Invoke("AttackUp", 0.75f);
            isAttacking = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        ChasePlayer();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsGround);
        isPlayerFront = Physics2D.OverlapCircle(attackBoxFront.position, 0.5f, playerLayer);
        isPlayerUp = Physics2D.OverlapCircle(attackBoxUp.position, 0.5f, playerLayer);
        if(lastAttack >= 1)
        {
            isAttacking = false;
        }
        else
        {
            lastAttack += Time.fixedDeltaTime;
        }
    }

    void ChasePlayer()
    {

        if(!isAttacking)
        {
            if (transform.position.x < player.transform.position.x)
            {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                if (facingRight)
                {
                    transform.localScale = new Vector2(-1, 1);
                    facingRight = false;
                }
            }
            else if (transform.position.x > player.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);

                if (!facingRight)
                {
                    transform.localScale = new Vector2(1, 1);
                    facingRight = true;
                }
            }
            if(isGrounded && isOnWall)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }
    private void AttackFront()
    {
        if(isPlayerFront)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
    private void AttackUp()
    {
        if (isPlayerUp)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackBoxFront.position, 0.5f);
        Gizmos.DrawWireSphere(attackBoxUp.position, 0.5f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }
}
