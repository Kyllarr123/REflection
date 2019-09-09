using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Vector3 jump;
    public float jumpForce;
    private Rigidbody rb;
    public bool isGrounded;
    public bool abilityActive;
    private Transform playerTrans;
    public Vector3 flipObjectSize;
    public float moveDist;
    public float coolDown;
    public bool onCd = false;
    int layerMask = 1 << 8;
    private Vector3 normGrav;

    //Camera
    public GameObject cam;
    private CameraBehaviour camB;
    
    //Moveable Ability
    private GameObject currentMoveable;
    
    //Clickables
    public bool canClick = false;
    public Button button;

    private void Awake()
    {
        playerTrans = transform;
        camB = cam.GetComponent<CameraBehaviour>();
    }

    void Start () {
        rb = GetComponent <Rigidbody> ();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        normGrav = Physics.gravity;
    }
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }

    void Update () {
        
        float x = Input.GetAxis ("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            
        }



        rb.velocity = new Vector3 (x * speed, rb.velocity.y, 0);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentMoveable != null)
            {
                Rigidbody moveRb = currentMoveable.GetComponent<Rigidbody>();

                MoveableObject move = currentMoveable.GetComponent<MoveableObject>();
                moveRb.constraints = move.rbCs;
                move.player = gameObject;
                move.linkedToPlayer = true;
            }

            if (canClick)
            {
                button.clicked = true;
            }
            
            
        }



    }


//use reflection ability
//flip player to the otherside of path
//inverse gravity
    public void Flip()
    {
        int reflectSurface = LayerMask.GetMask("Flipable");
        Debug.Log("do flip");
        if (!abilityActive && isGrounded && !onCd)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.gameObject.transform.position, Vector3.down * 10f, out hit,10f, layerMask))
            {
                camB.offset.y = -4;
                Renderer rend = hit.collider.GetComponent<Renderer>();
                flipObjectSize = rend.bounds.size;
                moveDist = flipObjectSize.y * 2;
                Vector3 currentPos = transform.position;
                currentPos.y = currentPos.y - moveDist;
                    
                transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
                Physics.gravity *= -1;
                jump = -jump;
                onCd = true;
                abilityActive = true;
                StartCoroutine(CD(coolDown));

                Debug.Log("on Reflecter"); 

            }

        }

        if (abilityActive && isGrounded && !onCd)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.gameObject.transform.position, Vector3.up * 10f, out hit, 10f,layerMask))
            {
                camB.offset.y = 4;
                Renderer rend = hit.collider.GetComponent<Renderer>();
                flipObjectSize = rend.bounds.size;
                moveDist = flipObjectSize.y * 2;
                Vector3 currentPos = transform.position;
                currentPos.y = currentPos.y + moveDist;
                    
                transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
                //
                jump = new Vector3(0.0f, 2.0f, 0.0f);
                onCd = true;
                abilityActive = false;
                Physics.gravity = normGrav;
                StartCoroutine(CD(coolDown));

                Debug.Log("on Reflecter"); 

            }

        }


    }

    public IEnumerator CD(float Cooldown)
    {
        yield return new WaitForSeconds(Cooldown);
        onCd = false;
    }


    public void PushObject()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Moveable")
        {

            currentMoveable = other.gameObject;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Moveable")
        {
            MoveableObject move = currentMoveable.GetComponent<MoveableObject>();
            move.player = null;
            move.linkedToPlayer = false;
            currentMoveable = null;

        }
    }
}
