using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDealDamage : MonoBehaviour
{
    int damage;
    private void Start()
    {
        damage = gameObject.GetComponentInParent<BatManager>().damage;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
