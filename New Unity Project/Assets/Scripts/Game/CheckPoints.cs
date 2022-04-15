using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    CheckPointManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            manager.checkpoint = transform.position;
        }
    }
}
