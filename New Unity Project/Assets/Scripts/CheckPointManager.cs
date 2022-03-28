using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Transform checkpoint;
    private void Start()
    {
        checkpoint = transform;
    }
}