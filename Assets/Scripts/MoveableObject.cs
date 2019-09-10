using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [HideInInspector] public RigidbodyConstraints rbCs;
    public GameObject linkedObj;
    private Rigidbody rb;
    public GameObject player;
    public bool linkedToPlayer;
    public float diviation;
    // Start is called before the first frame update
    
    
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rbCs = rb.constraints;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (linkedToPlayer)
        {
            rb.isKinematic = false;
            linkedObj.GetComponent<Rigidbody>().isKinematic = false;
            rb.mass = 0.1f;
            rb.velocity = player.GetComponent<Rigidbody>().velocity * 2f;
            Vector3 linkPos = linkedObj.transform.position;
            linkPos.x = gameObject.transform.position.x + diviation;
            linkPos.y = -gameObject.transform.position.y;
            linkedObj.transform.position = linkPos;
        }

        if (!linkedToPlayer)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            linkedObj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
