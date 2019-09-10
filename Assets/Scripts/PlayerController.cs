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
    public float x;
    public float rotateSpeed;
    public int runX;
    
    //Animator
    public Animator anim;

    public GameObject model;
    //Camera
    public GameObject cam;
    private CameraBehaviour camB;
    
    //Flip
    public bool flipped = false;
    
    //Moveable Ability
    private GameObject currentMoveable;
    
    //Clickables
    public bool canClick = false;
    public Button button;
    public bool cantMove = false;
    public bool canStomp;

    //Attack
    public GameObject sword;
    private void Awake()
    {
        playerTrans = transform;
        cam = FindObjectOfType<Camera>().gameObject;
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
        anim.SetBool("Grounded", true);
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }

    void Update () {
        if (cantMove == false)
        {
                    x = Input.GetAxis ("Horizontal");
                    
                    if (x != 0 && isGrounded)
                    {
                        anim.SetBool("Run", true);
                    }
            
                    if (x == 0 && isGrounded)
                    {
                        anim.SetBool("Run", false);
                    }
                    
                    if (x > 0)
                    {
                        transform.rotation = Quaternion.Euler( 0,180,0);
                        
                        if (abilityActive)
                        {
                            model.transform.localRotation = Quaternion.Euler(0f, 90f,0f );
                        }
                        else
                        {
                            model.transform.localRotation = Quaternion.Euler(0f, -90f,0f );
                        }
            
                    }
            
                    if (x < 0)
                    {
                        transform.rotation = Quaternion.Euler( 0,-180,0);
                        
                        if (abilityActive)
                        {
                            model.transform.localRotation = Quaternion.Euler(0f, -90f,0f );
                        }
                        else
                        {
                            model.transform.localRotation = Quaternion.Euler(0f, 90f,0f );
                        }
                    }
                    if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
                    {
                        isGrounded = false;
                        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                        anim.SetBool("Jumping", true);
                        anim.SetBool("Grounded", false);
                    }
                    model.transform.localPosition = new Vector3(0f,-1,0f);
                    if (abilityActive)
                    {
                        transform.rotation = Quaternion.Euler(180f, 0,0);
                    }
                    
            
            
            
                    rb.velocity = new Vector3 (x * speed, rb.velocity.y, 0);
            
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Flip();
                        
                        //gameObject.transform.localRotation = Quaternion.Euler(0, 90 ,-180);
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
                            anim.SetBool("Pushing", true);
                        }
            
                        if (canClick)
                        {
                            cantMove = true;
                            anim.SetTrigger("PushButton");
                            //button.clicked = true;
                            StartCoroutine(Wait());
                        }

                        if (canStomp)
                        {
                            cantMove = true;
                            anim.SetTrigger("StompButton");
                            //button.clicked = true;
                            StartCoroutine(Wait());
                        }
                    }
            
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
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
                camB.offset.y = -2;
                Renderer rend = hit.collider.GetComponent<Renderer>();
                flipObjectSize = rend.bounds.size;
                moveDist = flipObjectSize.y * 2;
                Vector3 currentPos = transform.position;
                currentPos.y = currentPos.y - moveDist;
                    
                transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
                //transform.rotation = Quaternion.Euler(-180, 0,0);
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
                camB.offset.y = 2;
                Renderer rend = hit.collider.GetComponent<Renderer>();
                flipObjectSize = rend.bounds.size;
                moveDist = flipObjectSize.y * 2;
                Vector3 currentPos = transform.position;
                currentPos.y = currentPos.y + moveDist;
                    
                transform.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
                //transform.rotation = Quaternion.Euler(180, 0,0);
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
            anim.SetBool("Pushing", false);

        }
    }

    public void Attack()
    {
        //Instantiate(sword, this.transform.position, Quaternion.identity);
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        cantMove = false;
        anim.SetTrigger("AnimDone");
    }
}
