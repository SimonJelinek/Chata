using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : ScreenBase
{
    public void ShowSettings()
    {
        // App.screenManager.Hide<MenuScreen>();
        App.screenManager.Show<SettingsScreen>();
    }

    public void StartLevel(string levelName)
    {
        App.gameManager.LoadScene(levelName, new ShowScreenCommand<InGameScreen>(), true);
        Hide();
    }
}
