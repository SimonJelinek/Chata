using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    void Start()
    {
        App.screenManager.Show<MenuScreen>();
    }

    public void ShowSettings()
    {
        App.screenManager.Hide<MenuScreen>();
        App.screenManager.Show<SettingsScreen>();
    }

    public void ReturnToMenu()
    {
        App.screenManager.Hide<SettingsScreen>();
        App.screenManager.Show<MenuScreen>();
    }
}
