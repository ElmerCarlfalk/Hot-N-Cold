using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [Header("Offset")]
    public float newOffset;
    private float normalOffset;

    public GameObject camPos;

    [Header("Lock Y")]
    public bool lockY;
    public float lockHeight;

    [Header("Boss Room")]
    public bool bossRoom = false;
    public GameObject currentCamera;
    public GameObject bossCamera;
    public GameObject door;

    [Header("On Start")]
    public bool activateCamera = false;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if(player != null)
        {
            if (bossRoom)
            {
                BossRoom();
            }
            else
            {
                if (lockY)
                {
                    Lock();
                }
                else
                {
                    Unlock();
                }

                if (activateCamera)
                {
                    CameraTransitionManager.Instance.ActivateCamera();
                }
            }
        }
    }

    private void Lock()
    {
        CameraTransitionManager.Instance.lockY = true;
        CameraTransitionManager.Instance.cameraLockPos.y = lockHeight;
        PlayerCameraManager.Instance.canLook = false;
    }

    private void Unlock() //Also changes CamPos Offset
    {
        Vector2 camPosOffset = camPos.transform.localPosition;
        camPosOffset.y = normalOffset + newOffset;
        camPos.transform.localPosition = camPosOffset;
        PlayerCameraManager.Instance.normalOffset = camPosOffset.y;
        CameraTransitionManager.Instance.lockY = false;
        PlayerCameraManager.Instance.canLook = true;
    }

    void BossRoom()
    {
        currentCamera.SetActive(false);
        bossCamera.SetActive(true);
        door.SetActive(true);
        PlayerCameraManager.Instance.canLook = false;
    }
}
