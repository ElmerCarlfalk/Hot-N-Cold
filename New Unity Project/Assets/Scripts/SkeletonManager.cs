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

    public Transform attackBoxFront;
    public bool isPlayerFront;
    public Transform attackBoxUp;
    public bool isPlayerUp;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (isPlayerFront && !isAttacking) AttackFront();
        else if (isPlayerUp && !isAttacking) AttackUp();

        ChasePlayer();
    }
    private void Update()
    {
        isPlayerFront = Physics2D.OverlapBox(attackBoxFront.position, new Vector2(1.5f, 1), playerLayer);
        isPlayerUp = Physics2D.OverlapBox(attackBoxUp.position, new Vector2(1.5f, 1), playerLayer);
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
        isAttacking = true;
        Invoke("StopAttacking", 1f);
    }
    private void AttackUp()
    {
        Debug.Log("Up");
        isAttacking = true;
        Invoke("StopAttacking", 1f);
    }
    private void StopAttacking()
    {
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackBoxFront.position, new Vector2(1.5f, 1));
        Gizmos.DrawWireCube(attackBoxUp.position, new Vector2(1.5f, 1));
    }
}
