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
    public bool shakeScreen = false;
    public float shakeIntensity;
    public float shakeDuration;
    public GameObject takeDamageEffect;

    protected Animator animator;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(takeDamageEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(0, 0, 0));
        if (shakeScreen)
        {
            CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeDuration);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
