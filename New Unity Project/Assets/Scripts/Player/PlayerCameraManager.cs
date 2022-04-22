using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public static PlayerCameraManager Instance { get; set; }

    [Header("Cooldowns")]
    public float timeUntilLook;
    private float timeUntilLookCounter;

    [Header("Offsets")]
    public float lookUpOffset;
    public float lookDownOffset;
    [HideInInspector]
    public float normalOffset;

    public GameObject camPos;

    [HideInInspector]
    public bool canLook;

    void Start()
    {
        Instance = this;
        normalOffset = camPos.transform.localPosition.y;
        timeUntilLookCounter = timeUntilLook;
        canLook = true;
    }

    void Update()
    {
        if (canLook)
        {
            if (!Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                    {
                        LookDown();
                    }
                    else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                    {
                        LookUp();
                    }
                    else
                    {
                        timeUntilLookCounter = timeUntilLook;
                        Vector2 camPosOffset = camPos.transform.localPosition;
                        camPosOffset.y = normalOffset;
                        camPos.transform.localPosition = camPosOffset;
                    }
                }
                else
                {
                    timeUntilLookCounter = timeUntilLook;
                    Vector2 camPosOffset = camPos.transform.localPosition;
                    camPosOffset.y = normalOffset;
                    camPos.transform.localPosition = camPosOffset;
                }
            }
            else
            {
                timeUntilLookCounter = timeUntilLook;
                Vector2 camPosOffset = camPos.transform.localPosition;
                camPosOffset.y = normalOffset;
                camPos.transform.localPosition = camPosOffset;
            }
        }
    }

    void LookDown()
    {
        if (timeUntilLookCounter <= 0)
        {
            Vector2 camPosOffset = camPos.transform.localPosition;
            camPosOffset.y = normalOffset - lookDownOffset;
            camPos.transform.localPosition = camPosOffset;
        }
        else
        {
            timeUntilLookCounter -= Time.deltaTime;
        }
    }

    void LookUp()
    {
        if (timeUntilLookCounter <= 0)
        {
            Vector2 camPosOffset = camPos.transform.localPosition;
            camPosOffset.y = normalOffset + lookUpOffset;
            camPos.transform.localPosition = camPosOffset;
        }
        else
        {
            timeUntilLookCounter -= Time.deltaTime;
        }
    }
}
