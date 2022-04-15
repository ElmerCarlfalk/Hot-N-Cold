using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Vector2 checkpoint;

    private void Start()
    {
        checkpoint = transform.position;
    }
}