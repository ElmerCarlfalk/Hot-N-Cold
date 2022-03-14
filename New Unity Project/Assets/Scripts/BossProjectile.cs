using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb;

    public float moveSpeed;

    public float veldamp;
    public float stifness;
    public float damp;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 dir = (player.transform.position - transform.position);
        float x = dir.magnitude;
        dir.Normalize();
        float v = Vector2.Dot(rb.velocity, dir);
        rb.velocity = rb.velocity * (1 - veldamp * Time.fixedDeltaTime) + dir * (stifness * x - damp * v) * Time.fixedDeltaTime;

        if (rb.velocity.magnitude > moveSpeed)
        {
            Vector3 vel = rb.velocity;
            vel.Normalize();
            rb.velocity = vel * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
    }
}
