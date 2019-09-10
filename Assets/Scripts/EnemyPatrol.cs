using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    public bool isStopped;
    public GameObject model;
    public Animator anim;

    public bool disabled = false;
    
    //Waypoint System
    public float actualSpeed = 2.0f;
    public GameObject[] checkpoints;
    int counter = 0;
    public float distance = 2.0f;
    
    //View Mechanic
    public float lookDistance;
    public delegate void FoundYou();
    public static event FoundYou GetRekt;
    private void Awake()
    {
        
    }

    void Start()
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        //agent.autoBraking = false;

        //GotoNextPoint();
    }
    
    void FixedUpdate ()
    {
        Vector3 direction = Vector3.zero;
       
        direction = checkpoints[counter].transform.position - transform.position;
        transform.LookAt(checkpoints[counter].gameObject.transform);
        if(direction.magnitude < distance)
        {
            if(counter < checkpoints.Length-1) //switch to the nex waypoint if exists
            {
                StartCoroutine(Looking());
                counter++;
            }
            else //begin from new if we are already on the last waypoint
            {
                StartCoroutine(Looking());
                counter = 0;
            }
        }
        direction = direction.normalized;
        Vector3 dir = direction;
        if (!isStopped)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(direction.x * actualSpeed, 0,0);
            anim.SetBool("Stopped", false);
        }

        Detect();


    }

    public void Detect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookDistance))
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                Debug.Log("fuck you player");
                //GetRekt();
            }
        }
    }




    IEnumerator Looking()
    {
        Debug.Log("Got to coroutine");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isStopped = true;
        anim.SetBool("Stopped", true);
        yield return new WaitForSeconds(5f);
        isStopped = false;
        //GotoNextPoint();
    }
}

