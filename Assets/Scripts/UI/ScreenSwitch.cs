using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    public void ShowSettings()
    {
        App.screenManager.Hide<MenuScreen>();
        App.screenManager.Show<SettingsScreen>();
    }
}
