using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen_NEW : MonoBehaviour
{
    bool fullScreen = true;

    public void ChangeScreen()
    {
        fullScreen =! fullScreen;
        Screen.fullScreen = fullScreen;
    }
}
