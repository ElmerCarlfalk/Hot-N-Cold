using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainProjectile : MonoBehaviour
{
    public int damage;
    public float fallSpeed;
    public float rotationSpeed;
    private int rotDir;

    private Rigidbody2D rb;

    public GameObject takeDamageEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * fallSpeed * 10 * Time.fixedDeltaTime;
        rotDir = Random.Range(-1, 1);
        if (rotDir == 0)
        {
            rotDir = 1;
        }
        rotationSpeed *= rotDir;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        else //Om det ?r golv
        {
            Instantiate(takeDamageEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
