using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravCheck : MonoBehaviour
{
    private Rigidbody rb;
    public bool antigrav = false;

    public bool grav = false;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (antigrav)
        {
            rb.AddForce(0,25,0);
        }
        if (grav)
        {
            rb.AddForce(0,-25,0);
        }
    }
}
