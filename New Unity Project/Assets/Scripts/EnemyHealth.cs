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
                currentVel.y = knockbackDir.y * knockback;
                rb.velocity = currentVel;
                //Add way to connect to scripts by changing one public somehow
            }
            else
            {
                Vector2 currentVel = rb.velocity;
                currentVel.x = knockbackDir.x * knockback;
                rb.velocity = currentVel;
                //Add way to connect to scripts by changing one public somehow
            }
        }
        else if (takeKnockbackGround)
        {
            knockbackDir.y = 0;
            rb.velocity = rb.velocity + (knockbackDir * knockback);
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
