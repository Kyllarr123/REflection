using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Scene retryScene;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EnemyPatrol.GetRekt += GameOverScene;
    }

    public void GoToGame()
    {
       SceneManager.LoadScene("GameSpace"); 
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GameOverScene()
    {
        retryScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("GameOver");
        //retryButton.
    }

    public void RetryScene()
    {
        SceneManager.LoadScene(retryScene.name);
    }
    
    
    
    
}
