using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
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

    public void RetryLevel()
    {
        menu.RetryScene();
    }
}
