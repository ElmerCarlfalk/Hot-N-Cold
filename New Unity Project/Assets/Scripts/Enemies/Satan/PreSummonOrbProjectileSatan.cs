using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSummonOrbProjectileSatan : MonoBehaviour
{
    private float timeUntilSummonAttack;

    [Header("Projectile")]
    public GameObject summonProjectile;

    [Header("Particle Effects")]
    public GameObject summoningParticlesOut;
    public GameObject summoningParticlesIn;
    public GameObject summonParticles;

    void Start()
    {
        Instantiate(summoningParticlesIn, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.Euler(0, 0, 0));
        Instantiate(summoningParticlesOut, transform.position, Quaternion.Euler(0, 0, 0));
        timeUntilSummonAttack = summoningParticlesOut.GetComponent<ParticleSystem>().main.duration;
    }

    void Update()
    {
        if (timeUntilSummonAttack <= 0)
        {
            Instantiate(summonParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.Euler(0, 0, 0));
            Instantiate(summonProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            timeUntilSummonAttack -= Time.deltaTime;
        }
    }
}
