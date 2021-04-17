using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : ScreenBase
{


    public void GoBack()
    {
        App.screenManager.Hide<SettingsScreen>();
        //App.screenManager.Show<MenuScreen>();
    }
}
