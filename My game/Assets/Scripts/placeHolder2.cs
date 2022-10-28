using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class placeHolder2 : MonoBehaviour
{

    public Transform target;


    public Quaternion offsetRot;
    public Vector3 offset;



    //cardinal directions
    public bool movingForward = false;
    public bool movingBackward = false;
    public bool movingLeft = false;
    public bool movingRight = false;

    public float smoothSpeed = 0.125f;
    public float smoothRot = 0.125f;

    //public modifyScript m;

    // Start is called before the first frame update
    void Start()
    {
        offsetRot = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //camera movement smoothing
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //camera rotation smoothing
        Quaternion desiredRotation = transform.rotation * offsetRot;
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;

    }

    void Update()
    {
        cardinalDirections();
        movementModifier();
    }
    //void OnCollisionEnter()
    //{
    //    //normalCameraFollow = false;
    //}


    void cardinalDirections()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movingForward = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            movingForward = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            movingBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            movingBackward = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            movingRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
        }
    }

    void movementModifier()
    {
        if (movingForward == true && movingBackward == false)
        {
            Quaternion forwardPos = Quaternion.Euler(30, -90, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, forwardPos, smoothRot);

            movingBackward = false;

            if (transform.rotation == forwardPos)
            {
                movingForward = false;
            }
        }

        if (movingBackward == true && movingForward == false)
        {
            Quaternion backwardPos = Quaternion.Euler(60, -90, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, backwardPos, smoothRot);

            movingForward = false;

            if (transform.rotation == backwardPos)
            {
                movingBackward = false;
            }
        }

        if (movingLeft == true && movingRight == false)
        {
            Quaternion leftPos = Quaternion.Euler(45, -115, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, leftPos, smoothRot);

            movingRight = false;

            if (transform.rotation == leftPos)
            {
                movingLeft = false;
            }
        }

        if (movingRight == true && movingLeft == false)
        {
            Quaternion rightPos = Quaternion.Euler(45, -65, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, rightPos, smoothRot);

            movingLeft = false;

            if (transform.rotation == rightPos)
            {
                movingRight = false;
            }
        }

        //diagonal

        if (movingForward == true && movingLeft == true)
        {
            Quaternion northWestPos = Quaternion.Euler(30, -115, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, northWestPos, smoothRot);

            if (transform.rotation == northWestPos)
            {
                movingForward = false;
                movingLeft = false;
            }
        } // northwest

        if (movingForward == true && movingRight == true)
        {
            Quaternion northEastPos = Quaternion.Euler(40, -75, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, northEastPos, smoothRot);

            if (transform.rotation == northEastPos)
            {
                movingForward = false;
                movingRight = false;
            }
        } // northeast

        if (movingBackward == true && movingLeft == true)
        {
            Quaternion southWestPos = Quaternion.Euler(60, -100, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, southWestPos, smoothRot);

            if (transform.rotation == southWestPos)
            {
                movingBackward = false;
                movingLeft = false;
            }
        } // southwest

        if (movingBackward == true && movingRight == true)
        {
            Quaternion southEastPos = Quaternion.Euler(55, -60, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, southEastPos, smoothRot);

            if (transform.rotation == southEastPos)
            {
                movingBackward = false;
                movingRight = false;
            }
        } // southeast

        if (movingBackward == false && movingForward == false && movingLeft == false && movingRight == false)
        {
            Quaternion restingPos = Quaternion.Euler(50, -90, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, restingPos, smoothRot);
        } // resting pos  
    }

}
