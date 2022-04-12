using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    public int damage;
    public float timeUntilAttack;

    private Animator animator;

    private bool playerIsTouching;
    private PlayerHealth player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (timeUntilAttack <= 0)
        {
            animator.SetBool("Attack", true);
            if(playerIsTouching == true)
            {
                player.TakeDamage(damage);
            }
        }
        else
        {
            timeUntilAttack -= Time.deltaTime;
        }

        if(animator.speed == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if(player != null)
        {
            playerIsTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            playerIsTouching = false;
        }
    }
}
