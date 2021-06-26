﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public Vector2 checkPoint;

    public int nextSceneLoad;

    void Awake()
    {
        App.checkpoints = this;
        checkPoint = transform.position;
    }

    void Start() 
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            checkPoint = transform.position;
        }
        if (collision.gameObject.tag == "LevelPass")
        {
            if (SceneManager.GetActiveScene()==SceneManager.GetSceneByName("Level1"))
            {
                App.levelLoader.Load("Level2");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                //App.levelLoader.Load("Level3");
            }

            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt")) 
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
}
