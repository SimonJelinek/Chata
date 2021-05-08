using System.Collections;
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

        crosshair.SetActive(false);
        Cursor.visible = true;
    }

    public void GoBack()
    {
        pauseScreen.SetActive(false);

        crosshair.SetActive(true);
        Cursor.visible = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
