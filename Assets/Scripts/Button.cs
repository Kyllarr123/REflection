using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject linkedButton;

    public GameObject thisDoor;

    public GameObject linkedDoor;    
    
    public GameObject thisDoor1;

    public GameObject linkedDoor1;

    public bool clicked = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            Destroy(thisDoor);
            Destroy(linkedDoor);
            if (thisDoor1 != null && linkedDoor1 != null)
            {
                Destroy(thisDoor1);
                Destroy(linkedDoor1);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (this.gameObject.tag == "Button")
            {
                controller.canClick = true;
                controller.button = this;  
            }

            if (this.gameObject.tag == "PressurePlate")
            {
                controller.canStomp = true;
                controller.button = this;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            controller.canClick = false;
            controller.canStomp = false;
        }
    }
}
