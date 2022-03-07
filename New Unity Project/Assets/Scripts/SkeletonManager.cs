using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public LayerMask playerLayer;
    public float speed;
    public float attackRange;
    public bool facingRight = true;
    public bool isAttacking = false;

    private Rigidbody2D rb;

    public Transform groundCheck;
    bool isGrounded;
    bool isOnWall;
    public Transform attackBoxFront;
    bool isPlayerFront;
    public Transform attackBoxUp;
    bool isPlayerUp;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (isPlayerFront && !isAttacking)
        {
            Invoke("AttackFront", 0.75f);
            isAttacking = true;
        }
        else if (isPlayerUp && !isAttacking)
        {
            Invoke("AttackUp", 0.75f);
            isAttacking = true;
        }

        ChasePlayer();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle()
        isPlayerFront = Physics2D.OverlapCircle(attackBoxFront.position, 0.5f, playerLayer);
        isPlayerUp = Physics2D.OverlapCircle(attackBoxUp.position, 0.5f, playerLayer);
    }

    void ChasePlayer()
    {

        if(!isAttacking)
        {
            if (transform.position.x < player.transform.position.x)
            {
                //if(transform.position.x - player.transform.position.x < -attackRange)
                //{
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                //}
                if (!facingRight)
                {
                    transform.localScale = new Vector2(1, 1);
                    facingRight = true;
                }
            }
            else if (transform.position.x > player.transform.position.x)
            {
                //if (transform.position.x - player.transform.position.x > attackRange)
                //{
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                //}
                if (facingRight)
                {
                    transform.localScale = new Vector2(-1, 1);
                    facingRight = false;
                }
            }
        }
    }
    private void AttackFront()
    {
        Debug.Log("Front");
        Invoke("StopAttacking", 0.25f);
    }
    private void AttackUp()
    {
        Debug.Log("Up");
        Invoke("StopAttacking", 0.25f);
    }
    private void StopAttacking()
    {
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackBoxFront.position, 0.5f);
        Gizmos.DrawWireSphere(attackBoxUp.position, 0.5f);
    }
}
