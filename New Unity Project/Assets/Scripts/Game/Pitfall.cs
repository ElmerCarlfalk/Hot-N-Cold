using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    public int numberOfTiles;
    public float timeBeforeRemoval = 0.3f;
    public float shakeAmount = 5;

    bool active = false;
    public Transform leftTile;
    Transform[] Tiles;
    public float originalLeftTile;

    public GameObject destroyParticles;

    private void Start()
    {
        originalLeftTile = leftTile.position.x;
        Tiles = new Transform[numberOfTiles];
    }
    private void FixedUpdate()
    {
        if(active)
        {
            Vector2 newPos = Random.insideUnitCircle * (Time.deltaTime * shakeAmount);
            transform.position = newPos;
            Invoke("Remove", timeBeforeRemoval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            active = true;
        }
    }
    private void Remove()
    {

        for (int i = 0; i < numberOfTiles; i++)
        {
            Tiles[i] = leftTile;
            Tiles[i].position = new Vector3(leftTile.position.x + i, leftTile.position.y, -1);
            Instantiate(destroyParticles, Tiles[i].position, Quaternion.Euler(0, 0, 0));
            leftTile.position = new Vector2(originalLeftTile, leftTile.position.y);
        }
        Destroy(gameObject);
    }

}
