using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScreen_NEW : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject crosshair;

    bool pause = false;

    void Awake()
    {
        App.inGameScreen_NEW = this;
    }

    public void ShowPauseScreen()
    {
        pause = !pause;

        if (pause)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            crosshair.SetActive(false);
            Cursor.visible = true;
            gameObject.SetActive(false);
        }
        else
        {
            GoBack();
        }
    }

    public void GoBack()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        crosshair.SetActive(true);
        Cursor.visible = false;
        gameObject.SetActive(true);
        Debug.Log("Unpause");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
