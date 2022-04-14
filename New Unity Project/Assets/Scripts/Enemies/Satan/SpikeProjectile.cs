using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    public int damage;
    public float timeUntilAttack;

    private Animator animator;

    private bool hasAttacked = false;

    private bool playerIsTouching;
    private PlayerHealth player;

    public Transform pointOfParticles;
    public GameObject particleEffectEnter;
    public GameObject particleEffectAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //pointOfParticles = GetComponentInChildren<Transform>();
        Instantiate(particleEffectEnter, pointOfParticles.position, Quaternion.Euler(-90, 0, 0));
    }

    private void Update()
    {
        if (timeUntilAttack <= 0)
        {
            animator.SetBool("Attack", true);
            if (playerIsTouching == true)
            {
                player.TakeDamage(damage);
            }

            if (!hasAttacked)
            {
                hasAttacked = true;
                Instantiate(particleEffectAttack, pointOfParticles.position, Quaternion.Euler(-90, 0, 0));
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
