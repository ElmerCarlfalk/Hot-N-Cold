using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    int currentHealth;

    [Header("Combat")]
    public int damage;
    public bool canPogo = true;
    public GameObject takeDamageEffect;

    [Header("On Spawn")]
    public GameObject spawnParticles;

    protected Animator animator;

    protected virtual void Start()
    {
        Instantiate(spawnParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, 0));
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(takeDamageEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, 0));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
