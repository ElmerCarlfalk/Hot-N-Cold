using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    public bool takeKnockbackAir = false;
    public bool takeKnockbackGround = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;

        if (takeKnockbackAir)
        {
                gameObject.GetComponent<BatManager>().knockbackDir = knockbackDir;
                gameObject.GetComponent<BatManager>().isStunned = true;
        }
        else if (takeKnockbackGround)
        {
            knockbackDir.y = 0;
            //rb.velocity = rb.velocity + (knockbackDir * knockback);
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
