using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamProjectile : MonoBehaviour
{
    public float timeUntilAttack;
    private bool isCharging;

    public int damage;
    public float attackSpeed;

    private Rigidbody2D rb;
    private GameObject Satan;

    bool dashRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Satan = GameObject.FindGameObjectWithTag("Satan");
        if(Satan.transform.position.x > transform.position.x)
        {
            dashRight = true;
        }
        else
        {
            dashRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCharging)
        {
            if (timeUntilAttack <= 0)
            {
                Charge();
                isCharging = true;
            }
            else
            {
                timeUntilAttack -= Time.fixedDeltaTime;
            }
        }
    }

    void Charge()
    {
        if (dashRight) //Om Satan är åt höger
        {
            rb.velocity = Vector2.right * attackSpeed * 10 * Time.fixedDeltaTime;
        }
        else //Om Satan är åt vänster
        {
            rb.velocity = Vector2.left * attackSpeed * 10 * Time.fixedDeltaTime;
        }
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
