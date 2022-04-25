using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance { get; private set; }
    public Image UIImage;

    [Header("Transition")]
    public float fadeInSpeed;
    public float fadeOutSpeed;
    private float alpha;
    private bool fadeGameIn = true;

    private float timeUntilFade = 0;

    public float timeUntilTransition;
    private int nextGameLevel;

    [Header("Flash")]
    public float flashSpeed;
    private bool flashGame = false;
    private bool flashingBack = false;

    private void Start()
    {
        Instance = this;
        fadeGameIn = true;
        alpha = UIImage.color.a;
    }

    private void Update()
    {
        if (!flashGame)
        {
            if (fadeGameIn)
            {
                if (alpha > 0)
                {
                    alpha -= Time.deltaTime * fadeInSpeed;
                    UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
                }
                else
                {
                    UIImage.enabled = false;
                }
            }
            else if (!fadeGameIn)
            {
                UIImage.enabled = true;

                if (timeUntilFade <= 0)
                {
                    if (alpha < 1)
                    {
                        alpha += Time.deltaTime * fadeOutSpeed;
                        UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
                    }
                    else //Extra Time for screen to be stay dark
                    {
                        if (timeUntilTransition <= 0)
                        {
                            SceneManager.LoadScene(nextGameLevel);
                        }
                        else
                        {
                            timeUntilTransition -= Time.deltaTime;
                        }
                    }
                }
                else
                {
                    timeUntilFade -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (!flashingBack)
            {
                if (alpha < 1)
                {
                    alpha += Time.deltaTime * flashSpeed;
                    UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
                }
                else
                {
                    flashingBack = true;
                }
            }
            else
            {
                if (alpha > 0)
                {
                    alpha -= Time.deltaTime * flashSpeed;
                    UIImage.color = new Color(UIImage.color.r, UIImage.color.g, UIImage.color.b, alpha);
                }
                else
                {
                    UIImage.enabled = false;
                    flashingBack = false;
                    flashGame = false;
                }
            }
        }
    }

    public void FadeImage(bool fadeIn, float timeUntilFadeStart, int nextScene)
    {
        alpha = UIImage.color.a;
        fadeGameIn = fadeIn;
        timeUntilFade = timeUntilFadeStart;
        nextGameLevel = nextScene;
    }

    public void FlashImage()
    {
        UIImage.enabled = true;
        alpha = UIImage.color.a;
        flashGame = true;
    }
}
