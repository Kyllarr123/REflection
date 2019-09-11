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
        SceneManager.LoadScene("Level1");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }


    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToCredits2()
    {
        SceneManager.LoadScene("Credits2");
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

    public void Level2()
    {
        SceneManager.LoadScene(4);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }





}
