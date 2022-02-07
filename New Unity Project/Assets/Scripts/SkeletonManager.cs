using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public bool facingRight = true;
    public bool isAttacking = false;

    private Rigidbody2D rb;

    public Transform[] patrolPoints;
    public GameObject player;

    public Collider2D AttackCheckFront;
    public Collider2D AttackCheckUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {

        if(!isAttacking)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);
                if (!facingRight)
                    transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > player.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);
                if (facingRight)
                    transform.localScale = new Vector2(-1, 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider collision)
    {
        if(collision.gameObject == player)
        {
            if (!isAttacking)
            {
                if (player.transform.position.y > transform.position.y + 1.2) AttackUp();

                else AttackFront();
                isAttacking = true;
            }
        }
    }
    private void AttackFront()
    {

    }
    private void AttackUp()
    {

    }
}
