using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    GameObject CheckPointManager;
    GameObject player;

    private void Start()
    {
        CheckPointManager = GameObject.FindGameObjectWithTag("CheckPointManager");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Trigger();
        }
    }
    void Trigger()
    {
        CheckPointManager.GetComponent<CheckPointManager>().checkpoint.position = transform.position;
    }
}
