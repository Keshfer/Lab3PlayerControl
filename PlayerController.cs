using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private float horizontalMove;
    private float verticalMove;
    private float speed = 5f;
    private CharacterController characterControl;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private float jumpPhysics;
    private float height = 2f;
    private bool isGround;
    private GameObject groundChecker;
    private float groundPull = 55f;
    private float gravityMod = 2f;
    public LayerMask theGround;
    private float xBoundary = 19f;
    private float zBoundary = 19f;
    

    // Start is called before the first frame update
    void Start()
    {
        characterControl = gameObject.GetComponent<CharacterController>();
        //GroundChecker gameobject needs to be the child position #2 in order for this to work properly.
        groundChecker = transform.GetChild(2).gameObject;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        isGround = Physics.CheckSphere(groundChecker.transform.position, 0.6f, theGround);
        print(isGround);
        //The formula gives the velocity needed to jump a certain height
        jumpPhysics = Mathf.Sqrt(height * -2f * Physics.gravity.y);
        
        
        if(isGround && velocity.y < 0)
        {
            velocity.y = -groundPull;
        } else
        {
            //This simulates gravity and applies it to the player
            velocity.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        }
        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = jumpPhysics;
            //print("Jump!");
        }
        //print(velocity);
        characterControl.Move(velocity * Time.deltaTime);
        characterControl.Move(move * speed * Time.deltaTime);
        
        //Constrains the player's movement to the boundaries
        if(transform.position.x >= xBoundary)
        {
            transform.position = new Vector3 (xBoundary, transform.position.y, transform.position.z);
        } else if (transform.position.x <= -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if(transform.position.z >= zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        } else if(transform.position.z <= -zBoundary)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, -zBoundary);
        }




        


    }
}
