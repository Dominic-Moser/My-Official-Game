using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class CharacterControllerScript : MonoBehaviour
{
    //simplemovement variables/getters
    public string controlMode = "null";
    CapsuleCollider capCol;
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float jumpSpeed = 3.5f;
    public CharacterController characterController;
    public bool canDoubleJump;
    public float doubleJumpMulti;
    private float yDirection;
    //dash chacanary
    float maxDelay = 0.5f;
    bool dashReady = false;
    public bool dash = false;
    //end simplemovement variables/getters


    void Start()
    {

    }

    void Update()
    {
        if(dash != true)
        {
            simpleMovementHandler();
        }
        dashHandler();
    }
    
    void simpleMovementHandler()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(verticalInput, 0, horizontalInput);
        if (Input.GetKey("left shift") && characterController.isGrounded)
        {
            speed = 12;
        }
        else
        {
            speed = 6;
        }
        if (characterController.isGrounded)
        {
            canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                yDirection = jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                yDirection = jumpSpeed * doubleJumpMulti;
                canDoubleJump = false;
            }
        }
        yDirection -= gravity * Time.deltaTime;
        direction.y = yDirection;
        characterController.Move(direction * speed * Time.deltaTime);
    }
    void dashHandler()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (dashReady)
                Dash();
            else
                PrepareDash(true);
        }
    }
    void Dash()
    {
        dashReady = false;
        Debug.Log("Hold Dash");
        //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here

        dash = false;
    }
    void PrepareDash(bool makeReady)
    {
        //this is where the handling happens
        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        dashReady = true;
        dash = true;
    }
    void CancelDash()
    {
        dashReady = false;
        dash = false;
    }
}