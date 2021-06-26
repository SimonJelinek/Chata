using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public Slider volumeSlider;

    public GameObject settingsScreen;
    public GameObject levelsScreen;

    public void StartLevel()
    {
       SceneManager.LoadScene("Level1");
    }

    public void ShowLevels() 
    {
        levelsScreen.SetActive(true);
    }

    public void HideLevels() 
    {
        levelsScreen.SetActive(false);
    }

    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void GoBack()
    {
        settingsScreen.SetActive(false);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
