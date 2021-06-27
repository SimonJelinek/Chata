using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject levelsScreen;
    public Animator transition;
    public float transitionTime;

    void Awake()
    {
        App.levelLoader = this;
        //PlayerPrefs.DeleteAll();
    }

    public void Load(string sceneName)
    {
        if (levelsScreen != null) 
        {
            levelsScreen.SetActive(false);
        }
        StartCoroutine(LoadScene(sceneName));
    }

    public void Reset() 
    {
        PlayerPrefs.SetInt("levelAt",1);
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
