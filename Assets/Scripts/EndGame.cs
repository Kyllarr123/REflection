using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private MenuManager menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                menu.Level1();
            }

            if (SceneManager.GetActiveScene().name == "Level2")
            {
                menu.Level2();
            }
        }

    }
}
