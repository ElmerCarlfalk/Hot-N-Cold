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
            rb.velocity = knockbackDir * knockback;
        }
        else if (takeKnockbackGround)
        {
            knockbackDir.y = 0;
            rb.velocity = knockbackDir * knockback;
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
