using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                gameIsPaused = false;
            }
            else
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }
    }

    public void StartButton()
    {
        FadeScreen.Instance.FadeImage(false, 0, 1);
    }

    public void MenuButton()
    {
        FadeScreen.Instance.FadeImage(false, 0, 0);
        Time.timeScale = 1f;
    }

    //Stänger av spelet
    public void QuitButton()
    {
        Application.Quit();
    }

    public void PauseButton()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
