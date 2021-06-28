using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public GameObject[] buttons;
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
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void HideLevels() 
    {
        levelsScreen.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void GoBack()
    {
        settingsScreen.SetActive(false);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
