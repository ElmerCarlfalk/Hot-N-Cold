using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public bool aggro = false;
    public bool facingRight = true;

    private Rigidbody2D rb;

    public GameObject[] patrolPoints;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!aggro) Patrol();
        else if (aggro) ChasePlayer();
        
    }
    void Patrol()
    {

    }
    void ChasePlayer()
    {
        if(transform.position.x - player.position.x > attackRange || transform.position.x - player.position.x < -attackRange)
        {
            if (transform.position.x < player.position.x)
            {
                rb.velocity = new Vector2(speed, 0);
                if (!facingRight)
                    transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > player.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);
                if (facingRight)
                    transform.localScale = new Vector2(-1, 1);
            }
        }
    }
}
