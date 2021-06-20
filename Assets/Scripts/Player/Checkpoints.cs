using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public Vector2 checkPoint;

    void Awake()
    {
        App.checkpoints = this;
        checkPoint = transform.position;
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
                PassLevel("Level2");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                Debug.Log("LoadLevel3");
            }
        }
    }

    void PassLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
