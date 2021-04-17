using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private ScreenBase[] screens;

    private void Awake()
    {
        screens = GetComponentsInChildren<ScreenBase>(true);
        App.screenManager = this;
    }

    private void Start()
    {
        Show<MenuScreen>();
    }

    public void Show<T>()
    {
        foreach(ScreenBase screen in screens)
        {
            if(screen.GetType() == typeof(T))
            {
                screen.Show();
            }
        }
    }

    public void Hide<T>()
    {
        foreach (ScreenBase screen in screens)
        {
            if (screen.GetType() == typeof(T))
            {
                screen.Hide();
            }
        }
    }
}
