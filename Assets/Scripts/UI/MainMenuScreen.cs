using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public GameObject settingsScreen;

    public void StartLevel()
    {
       SceneManager.LoadScene("Level1");
    }

    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void GoBack()
    {
        settingsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
