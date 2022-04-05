using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDealDamage : MonoBehaviour
{
    int enemyDamage;
    private void Start()
    {
        enemyDamage = gameObject.GetComponent<BatManager>().enemyDamage;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(enemyDamage);
        }
    }
}
