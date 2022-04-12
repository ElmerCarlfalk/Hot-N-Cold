using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    public float animationPlayTime;

    public int maxHealth;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject takeDamageEffect;

    public float invTime;
    private float invTimeCounter;
    bool isInv = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        invTimeCounter = invTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(animator.GetBool("Die") == true)
        {
            if (animationPlayTime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                animationPlayTime -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isInv)
        {
            if(invTimeCounter <= 0)
            {
                isInv = false;
                invTimeCounter = invTime;
            }
            else
            {
                invTimeCounter -= Time.fixedTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInv)
        {
            currentHealth -= damage;
            isInv = true;

            if (currentHealth <= 0)
            {
                Die();
            }
            if (animator.GetBool("Die") == false)
            {
                Instantiate(takeDamageEffect, transform.position, Quaternion.identity);
            }
        }
    }

    void Die()
    {
        animator.SetBool("Die", true);
        rb.velocity = Vector2.zero;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
    }
}
