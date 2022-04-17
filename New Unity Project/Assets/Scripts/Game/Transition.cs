using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if(player != null)
        {
            FadeScreen.Instance.FadeImage(false);
        }
    }
}
