using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float speed = 1f;

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    
    void Update()
    {
        //player camera movement
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        //}
    }

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);

        // on flip 
    }
}
