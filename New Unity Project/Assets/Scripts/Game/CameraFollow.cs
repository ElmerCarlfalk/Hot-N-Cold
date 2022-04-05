using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeedX = 2f;
    public float FollowSpeedYUp = 2f;
    public float FollowSpeedYDown = 2f;
    public float yOffset;
    public Transform target;

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        Vector3 xPos = Vector3.Slerp(transform.position, newPos, FollowSpeedX * Time.deltaTime);
        Vector3 yPos;

        if (newPos.y - transform.position.y > 0)
        {
            yPos = Vector3.Slerp(transform.position, newPos, FollowSpeedYUp * Time.deltaTime);
        }
        else
        {
            yPos = Vector3.Slerp(transform.position, newPos, FollowSpeedYDown * Time.deltaTime);
        }
        newPos.x = xPos.x;
        newPos.y = yPos.y;
        newPos.z = -10f;
        transform.position = newPos;
    }
}
