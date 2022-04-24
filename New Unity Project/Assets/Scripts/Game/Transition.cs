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
            Fade();
        }
    }

    void Fade()
    {
        FadeScreen.Instance.FadeImage(false, 0);
    }
}
