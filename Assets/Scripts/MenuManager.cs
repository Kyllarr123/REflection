using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToGame()
    {
       SceneManager.LoadScene("GameSpace"); 
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
