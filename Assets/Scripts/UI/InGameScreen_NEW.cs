﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScreen_NEW : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject crosshair;

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        crosshair.SetActive(false);
        Cursor.visible = true;
    }

    public void GoBack()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        crosshair.SetActive(true);
        Cursor.visible = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
