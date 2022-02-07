using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeedX = 2f;
    public float FollowSpeedY = 2f;
    public float yOffset;
    public Transform target;

    void FixedUpdate()
    {
        /*Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        newPos = transform.position;
        newPos.z = -10f;
        transform.position = newPos;*/

        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        Vector3 xPos = Vector3.Slerp(transform.position, newPos, FollowSpeedX * Time.deltaTime);
        Vector3 yPos = Vector3.Slerp(transform.position, newPos, FollowSpeedY * Time.deltaTime);
        newPos.x = xPos.x;
        newPos.y = yPos.y;
        newPos.z = -10f;
        transform.position = newPos;
    }
}
