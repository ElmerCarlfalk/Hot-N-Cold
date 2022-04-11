using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainProjectile : MonoBehaviour
{
    public int damage;

    public float fallSpeed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * fallSpeed * 10 * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        else //Om det är vägg
        {
            Destroy(gameObject);
        }
    }
}
