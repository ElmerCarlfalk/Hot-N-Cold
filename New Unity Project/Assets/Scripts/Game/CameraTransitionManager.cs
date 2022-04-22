using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    public static CameraTransitionManager Instance { get; set; }

    [HideInInspector]
    public bool lockY = false;

    [HideInInspector]
    public Vector2 cameraLockPos;

    public GameObject camPos;
    public GameObject cinemachine;

    void Start()
    {
        Instance = this;
    }

    public void ActivateCamera()
    {
        cinemachine.SetActive(true);
    }

    void Update()
    {
        if (lockY)
        {
            cameraLockPos.x = camPos.transform.position.x;
            camPos.transform.position = cameraLockPos;
        }
    }
}
