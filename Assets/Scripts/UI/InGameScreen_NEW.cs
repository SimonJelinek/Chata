using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScreen_NEW : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject crosshair;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeSelf == false)
            {
                //ShowPauseScreen();
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
                crosshair.SetActive(false);
                Cursor.visible = true;
                gameObject.SetActive(false);
            }

            if(pauseScreen.activeSelf == true)
            {
                //HidePauseScreen();
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
                crosshair.SetActive(true);
                Cursor.visible = false;
                gameObject.SetActive(true);
            }
        }
    }
    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        crosshair.SetActive(false);
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        crosshair.SetActive(true);
        Cursor.visible = false;
        gameObject.SetActive(true);
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
