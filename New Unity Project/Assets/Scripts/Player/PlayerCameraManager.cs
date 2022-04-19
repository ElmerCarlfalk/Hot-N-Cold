using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [Header("Cooldowns")]
    public float timeUntilLook;
    private float timeUntilLookCounter;
    public float confineTime;
    private float confineTimeCounter;

    [Header("Offsets")]
    public float lookUpOffset;
    public float lookDownOffset;
    private float normalOffset;

    public GameObject camPos;
    private Cinemachine.CinemachineConfiner cinemachine;

    void Start()
    {
        cinemachine = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<Cinemachine.CinemachineConfiner>();
        normalOffset = camPos.transform.localPosition.y;
        timeUntilLookCounter = timeUntilLook;
        confineTimeCounter = confineTime;
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                {
                    LookDown();
                }
                else if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    LookUp();
                }
                else
                {
                    timeUntilLookCounter = timeUntilLook;
                    Vector2 camPosOffset = camPos.transform.localPosition;
                    camPosOffset.y = normalOffset;
                    camPos.transform.localPosition = camPosOffset;
                    if (confineTimeCounter <= 0)
                    {
                        cinemachine.m_ConfineScreenEdges = true;
                    }
                    else
                    {
                        confineTimeCounter -= Time.deltaTime;
                    }
                }
            }
            else
            {
                timeUntilLookCounter = timeUntilLook;
                Vector2 camPosOffset = camPos.transform.localPosition;
                camPosOffset.y = normalOffset;
                camPos.transform.localPosition = camPosOffset;
                if (confineTimeCounter <= 0)
                {
                    cinemachine.m_ConfineScreenEdges = true;
                }
                else
                {
                    confineTimeCounter -= Time.deltaTime;
                }
            }
        }
        else
        {
            timeUntilLookCounter = timeUntilLook;
            Vector2 camPosOffset = camPos.transform.localPosition;
            camPosOffset.y = normalOffset;
            camPos.transform.localPosition = camPosOffset;
            if(confineTimeCounter <= 0)
            {
                cinemachine.m_ConfineScreenEdges = true;
            }
            else
            {
                confineTimeCounter -= Time.deltaTime;
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
            cinemachine.m_ConfineScreenEdges = false;
            confineTimeCounter = confineTime;
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
            cinemachine.m_ConfineScreenEdges = false;
            confineTimeCounter = confineTime;
        }
        else
        {
            timeUntilLookCounter -= Time.deltaTime;
        }
    }
}
