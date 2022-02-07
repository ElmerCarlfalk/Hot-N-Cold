using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPosUp;
    public Transform attackPosDown;
    public Transform attackPosHor;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    public float timeBtwAttack;
    private float startTimeBtwAttack;

    void Update()
    {
        if (startTimeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.W))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosUp.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                startTimeBtwAttack = timeBtwAttack;
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.S))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosDown.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                startTimeBtwAttack = timeBtwAttack;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosHor.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                startTimeBtwAttack = timeBtwAttack;
            }
        }
        else
        {
            startTimeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosHor.position, attackRange);
        Gizmos.DrawWireSphere(attackPosUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPosDown.position, attackRange);
    }
}
