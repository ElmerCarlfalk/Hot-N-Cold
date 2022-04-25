using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    public int damage;

    public float maxSpeed;

    public float veldamp;
    public float stifness;
    public float damp;

    public float timeBetweenParticles;
    private float timeBetweenParticlesCounter;
    public GameObject idleParticles;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timeBetweenParticlesCounter <= 0)
        {
            Instantiate(idleParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.Euler(0, 0, 0));
            timeBetweenParticlesCounter = timeBetweenParticles;
        }
        else
        {
            timeBetweenParticlesCounter -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 dir = (player.transform.position - transform.position);
        float x = dir.magnitude;
        dir.Normalize();
        float v = Vector2.Dot(rb.velocity, dir);
        rb.velocity = rb.velocity * (1 - veldamp * Time.fixedDeltaTime) + dir * (stifness - damp) * Time.fixedDeltaTime;

        if (rb.velocity.magnitude > maxSpeed)
        {
            Vector3 vel = rb.velocity;
            vel.Normalize();
            rb.velocity = vel * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
