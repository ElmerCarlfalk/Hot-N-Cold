using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float invTime;
    private float invTimeCounter;
    bool isInv = true;

    void Start()
    {
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
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
