using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDamage : MonoBehaviour
{
    private int damage;
    private void Start()
    {
        damage = GetComponentInParent<Enemy>().damage;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
