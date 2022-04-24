using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    GameObject player;
    public CheckPointManager checkPointManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkPointManager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(1);
            player.transform.position = checkPointManager.checkpoint;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            FadeScreen.Instance.FlashImage();
        }
    }
}
