using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance { get; private set; }
    public Image UIImage;

    public float fadeInSpeed;
    public float fadeOutSpeed;
    float alpha;
    bool fadeGameIn = true;

    public float timeUntilTransition;
    public int nextGameLevel;

    private void Start()
    {
        Instance = this;
        fadeGameIn = true;
        alpha = UIImage.color.a;
    }

    private void Update()
    {
        if (fadeGameIn)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeInSpeed;
                UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
            }
        }
        else if (!fadeGameIn)
        {
            if (alpha < 1)
            {
                alpha += Time.deltaTime * fadeOutSpeed;
                UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
            }
            else //Extra Time for screen to be stay dark
            {
                if(timeUntilTransition <= 0)
                {
                    SceneManager.LoadScene(nextGameLevel);
                }
                else
                {
                    timeUntilTransition -= Time.deltaTime;
                }
            }
        }
    }

    public void FadeImage(bool fadeIn)
    {
        alpha = UIImage.color.a;
        fadeGameIn = fadeIn;
    }
}
