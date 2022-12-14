using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

     public float walkSpeed;
    public float sprintSpeed;

    public bool sprinting;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float gravity = 9.81f;
    public ForceMode forceMode;
    bool forewardDashReady = false;
    bool backwardDashReady = false;
    bool leftDashReady = false;
    bool rightDashReady = false;

    [Header("Dash Stuff")]
    public bool dash = false;
    public float dashSpeed = 10f;
    public float maxDelay = 0.5f;

    [HideInInspector] public TextMeshProUGUI text_speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        dashHandler();
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        sprint();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        rb.AddForce(Vector3.down * gravity, ForceMode.Force);
    }

    private void sprint()
    {
        if(Input.GetKey(sprintKey))
        {
            sprinting = true;
            moveSpeed =  sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
            sprinting = false;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //this is where the handling happens
            CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
            Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
            backwardDashReady = true;

        }

    }

    void dashHandler()
    {
        if (Input.GetKeyUp(KeyCode.W) && grounded != true)
        {
            if (forewardDashReady)
            {
                forewardDash();
            }
            else
            {
                PrepareDashForeward(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) && grounded != true)
        {
            if (leftDashReady)
            {
                leftDash();
            }
            else
            {
                PrepareDashLeft(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.S) && grounded != true)
        {
            if (backwardDashReady)
            {
                backwardDash();
            }
            else
            {
                PrepareDashBackward(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) && grounded != true)
        {
            if (rightDashReady)
            {
                rightDash();
            }
            else
            {
                PrepareDashRight(true);
            }
        }
    }

    void forewardDash()
    {
        forewardDashReady = false;
        //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here
        rb.AddForce(-transform.right * dashSpeed, ForceMode.Acceleration);
        dash = false;
    }
    void backwardDash()
    {
        backwardDashReady = false;
        rb.AddForce(transform.right * dashSpeed, ForceMode.Acceleration);
        dash = false;
    }
    void leftDash()
    { 
        leftDashReady = false;
        rb.AddForce(-transform.forward * dashSpeed, ForceMode.Acceleration);
        dash = false;
    }
    void rightDash()
    {
        rightDashReady = false;
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Acceleration);
        dash = false;
    }

    void PrepareDashForeward(bool makeReady)
    {
        //this is where the handling happens
        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        forewardDashReady = true;
        dash = true;
    }
    void PrepareDashBackward(bool makeReady)
    {
        //this is where the handling happens
        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        backwardDashReady = true;
        dash = true;
    }
    void PrepareDashLeft(bool makeReady)
    {
        //this is where the handling happens
        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        leftDashReady = true;
        dash = true;
    }
    void PrepareDashRight(bool makeReady)
    {

        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        rightDashReady = true;
        dash = true;
    }
    void CancelDash()
    {
        leftDashReady = false;
        rightDashReady = false;
        forewardDashReady = false;
        backwardDashReady = false;
        //dash = false;
    }
}

//    void CancelDash()
//    {
//        leftDashReady = false;
//        rightDashReady = false;
//        forewardDashReady = false;
//        backwardDashReady = false;

//        //dash = false;
//    }
//}
////simplemovement variables/getters
//public string controlMode = "null";
//CapsuleCollider capCol;
//[SerializeField]
//private float speed = 6.0f;
//[SerializeField]
//private float gravity = 9.81f;
//[SerializeField]
//private float jumpSpeed = 3.5f;
//public CharacterController characterController;
//public bool canDoubleJump;
//public float doubleJumpMulti;
//private float yDirection;
////dash chacanary
//float maxDelay = 0.5f;
//bool forewardDashReady = false;
//bool backwardDashReady = false;
//bool leftDashReady = false;
//bool rightDashReady = false;
//public bool dash = false;
//public bool onGround = false;
//public bool isGliding = false;
////end simplemovement variables/getters


//void Start()
//{

//}

//void Update()
//{

//    simpleMovementHandler();

//    dashHandler();
//    if (characterController.isGrounded)
//    {
//        onGround = true;
//    }
//    if (!characterController.isGrounded)
//    {
//        onGround = false;
//    }

//    glidingMechanic();
//}

//void simpleMovementHandler()
//{
//    float horizontalInput = Input.GetAxis("Horizontal");
//    float verticalInput = -Input.GetAxis("Vertical");

//    Vector3 direction = new Vector3(verticalInput, 0, horizontalInput);
//    if (Input.GetKey("left shift") && characterController.isGrounded)
//    {
//        speed = 12;
//    }
//    else
//    {
//        speed = 6;
//    }
//    if (characterController.isGrounded)
//    {
//        canDoubleJump = true;
//        if (Input.GetButtonDown("Jump"))
//        {
//            yDirection = jumpSpeed;
//        }
//    }
//    else
//    {
//        if (Input.GetButtonDown("Jump") && canDoubleJump)
//        {
//            yDirection = jumpSpeed * doubleJumpMulti;
//            canDoubleJump = false;
//        }
//    }
//    yDirection -= gravity * Time.deltaTime;
//    direction.y = yDirection;
//    characterController.Move(direction * speed * Time.deltaTime);
//}
//void dashHandler()
//{
//    if (Input.GetKeyUp(KeyCode.W) && onGround != true)
//    {
//        if (forewardDashReady)
//        {
//            forewardDash();
//        }
//        else
//        {
//            PrepareDashForeward(true);
//        }
//    }
//    if (Input.GetKeyUp(KeyCode.A) && onGround != true)
//    {
//        if (leftDashReady)
//        {
//            leftDash();
//        }
//        else
//        {
//            PrepareDashLeft(true);
//        }
//    }
//    if (Input.GetKeyUp(KeyCode.S) && onGround != true)
//    {
//        if (backwardDashReady)
//        {
//            backwardDash();
//        }
//        else
//        {
//            PrepareDashBackward(true);
//        }
//    }
//    if (Input.GetKeyUp(KeyCode.D) && onGround != true)
//    {
//        if (rightDashReady)
//        {
//            rightDash();
//        }
//        else
//        {
//            PrepareDashRight(true);
//        }
//    }
//}
//void forewardDash()
//{
//    forewardDashReady = false;
//    //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here
//    characterController.Move(new Vector3(-10, 0, 0));
//    dash = false;
//}
//void backwardDash()
//{
//    backwardDashReady = false;
//    //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here
//    characterController.Move(new Vector3(10, 0, 0));
//    dash = false;
//}
//void leftDash()
//{
//    leftDashReady = false;
//    //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here
//    characterController.Move(new Vector3(0, 0, -10));
//    dash = false;
//}
//void rightDash()
//{
//    rightDashReady = false;
//    //dash action goes here - if your not cool like me, in wich case you wont have the common sense to put a function here
//    characterController.Move(new Vector3(0, 0, 10));
//    dash = false;
//}

//void PrepareDashForeward(bool makeReady)
//{
//    //this is where the handling happens
//    CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
//    Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
//    forewardDashReady = true;
//    dash = true;
//}
//void PrepareDashBackward(bool makeReady)
//{
//    //this is where the handling happens
//    CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
//    Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
//    backwardDashReady = true;
//    dash = true;
//}
//void PrepareDashLeft(bool makeReady)
//{
//    //this is where the handling happens
//    CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
//    Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
//    leftDashReady = true;
//    dash = true;
//}
//void PrepareDashRight(bool makeReady)
//{
//    //this is where the handling happens
//    CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
//    Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
//    rightDashReady = true;
//    dash = true;
//}

//void CancelDash()
//{
//    leftDashReady = false;
//    rightDashReady = false;
//    forewardDashReady = false;
//    backwardDashReady = false;

//    dash = false;
//}
//void glidingMechanic()
//{
//    if (isGliding == true)
//    {
//        gravity = 1;
//    }
//    else
//    {
//        gravity = 9.81f;
//    }
//}