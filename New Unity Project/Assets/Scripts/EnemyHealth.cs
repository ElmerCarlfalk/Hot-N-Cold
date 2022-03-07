using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    public bool takeKnockbackAir = false;
    public bool takeKnockbackGround = false;
    public float knockback;

    Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;

        if (takeKnockbackAir)
        {
            if(rb.velocity.y != 0)
            {
                Vector2 currentVel = rb.velocity;
                currentVel.y = knockbackDir.y * knockback * -1;
                rb.velocity = currentVel;
                gameObject.GetComponent<BatManager>().isStunned = true;
            }
            else
            {
                Vector2 currentVel = rb.velocity;
                currentVel.x = knockbackDir.x * knockback;
                rb.velocity = currentVel;
                gameObject.GetComponent<BatManager>().isStunned = true;
            }
        }
        else if (takeKnockbackGround)
        {
            knockbackDir.y = 0;
            rb.velocity = rb.velocity + (knockbackDir * knockback);
            gameObject.GetComponent<BatManager>().isStunned = true; //Change from batManager to SkeletonManager
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
