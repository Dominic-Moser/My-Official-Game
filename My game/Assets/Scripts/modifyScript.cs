using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.PackageManager;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class modifyScript : MonoBehaviour
{
    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();

    public placeHolder1 p;
    public placeHolder2 p2;

    public float camFOV = 70;

    public Camera camera;

    public float smoothSpeed = 0.125f;
    public float smoothRot = 0.125f;

    public CharacterControllerScript characterControllerScript;

    public bool normalCameraFollow = false;
    public bool farOutCameraFollow = false;
    public bool closeCameraFollow = false;

    private void Start()
    {
        normalCameraFollow = true;
    }

    void FixedUpdate()
    {
        if (characterControllerScript.sprinting == true)
        {
            if(camera.fieldOfView < camFOV + 20)
            {
                camera.fieldOfView += 2;
            }
        }
        else
        {
            if(camera.fieldOfView > camFOV)
            {
                camera.fieldOfView -= 1;
            }
        }

        if (normalCameraFollow == true)
        {
            Vector3 smoothed = Vector3.Lerp(transform.position, p.transform.position, smoothSpeed);
            transform.position = smoothed;


            Quaternion smoothedRot = Quaternion.Lerp(transform.rotation, p.transform.rotation, smoothRot);
            transform.rotation = smoothedRot;
        }

        if (farOutCameraFollow == true)
        {

            Vector3 smoothed = Vector3.Lerp(transform.position, p2.transform.position, smoothSpeed);
            transform.position = smoothed;


            Quaternion smoothedRot = Quaternion.Lerp(transform.rotation, p2.transform.rotation, smoothRot);
            transform.rotation = smoothedRot;
        }
    }




}


//code snippets

//Vector2 rot;
//public float turnSpeed = 5.0f;
//public Transform target;
//public Transform placeHolderTarget;

//public float smoothSpeed = 0.125f;
//public float smoothRot = 0.125f;

//public float thrust = 15;

//public Vector3 offset;
//public Quaternion offsetRot;

//public Vector3 placeHolderOffset;
//public Quaternion placeHolderOffsetRot;


//Ray ray;

////cardinal directions
//public bool movingForward = false;
//public bool movingBackward = false;
//public bool movingLeft = false;
//public bool movingRight = false;

//public bool normalCameraFollow = true;

//bool modifiedCameraFollow = false;

//bool calculatePosBool = true;

//public Collider bCol;

//public Rigidbody rb;

//public Collider sc;
//public Collider msc;

////camera movement smoothing
//if (normalCameraFollow)
//{
//    Vector3 desiredPosition = target.position + offset;
//    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
//    transform.position = smoothedPosition;
//}


////camera rotation smoothing
//Quaternion desiredRotation = transform.rotation * offsetRot;
//Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
//transform.rotation = smoothedRotation;

//RaycastHit hitInfo = new RaycastHit();
//if (Input.GetMouseButtonDown(0))
//{
//    RaycastHit hit;
//    if (Physics.Raycast(transform.position, transform.forward, out hit, 100) && Physics.Raycast(ray, out hitInfo))
//    {
//        Terrain.SetBlock(hit, new BlockAir());
//        Vector3 chunkPos = hit.point;
//        Chunk chunk = null;
//        chunks.TryGetValue(new WorldPos(Convert.ToInt32(chunkPos.x), Convert.ToInt32(chunkPos.y), Convert.ToInt32(chunkPos.z)), out chunk);

//        Debug.Log("Test");
//    }
//}

////placeholder camera movement smoothing
//if (normalCameraFollow == false)
//{
//    placeHolderOffset = new Vector3(0, 15, 0);
//    Vector3 desiredPosition = target.position + placeHolderOffset;
//    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
//    sc.transform.position = smoothedPosition;

//    //camera placeholder rotation smoothing
//    Quaternion desiredRotation = transform.rotation * placeHolderOffsetRot;
//    Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
//    sc.transform.rotation = smoothedRotation;

//    sc.transform.position = new Vector3(target.position.x, target.position.y + 15, target.position.z);
//    if (normalCameraFollow == true)
//    {
//        transform.position = sc.transform.position;
//    }

//}

//if (calculatePosBool == false)
//{
//    sc.transform.position = transform.position;
//}



//void OnTriggerEnter(Collider sc)
//{
//    modifiedCameraFollow = false;

//}

////void OnTriggerStay(Collider sc)
////{
////    normalCameraFollow = false;
////}

//void OnTriggerExit(Collider sc)
//{
//    normalCameraFollow = true;

//}

//void Update()
//{
//    cardinalDirections();

//    movementModifier();
//}

//void rotateForward()
//{
//    Quaternion forwardPos = Quaternion.Euler(10, -90, 0);

//    transform.rotation = Quaternion.Lerp(transform.rotation, forwardPos, smoothRot);
//}

//void checkIfCollision()
//{

//}

//void cardinalDirections()
//{
//    if (Input.GetKeyDown(KeyCode.W))
//    {
//        movingForward = true;
//    }
//    if (Input.GetKeyUp(KeyCode.W))
//    {
//        movingForward = false;
//    }

//    if (Input.GetKeyDown(KeyCode.S))
//    {
//        movingBackward = true;
//    }
//    if (Input.GetKeyUp(KeyCode.S))
//    {
//        movingBackward = false;
//    }

//    if (Input.GetKeyDown(KeyCode.A))
//    {
//        movingLeft = true;
//    }
//    if (Input.GetKeyUp(KeyCode.A))
//    {
//        movingLeft = false;
//    }

//    if (Input.GetKeyDown(KeyCode.D))
//    {
//        movingRight = true;
//    }
//    if (Input.GetKeyUp(KeyCode.D))
//    {
//        movingRight = false;
//    }
//}

//void movementModifier()
//{
//    if (movingForward == true && movingBackward == false)
//    {
//        Quaternion forwardPos = Quaternion.Euler(10, -90, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, forwardPos, smoothRot);

//        movingBackward = false;

//        if (transform.rotation == forwardPos)
//        {
//            movingForward = false;
//        }
//    }

//    if (movingBackward == true && movingForward == false)
//    {
//        Quaternion backwardPos = Quaternion.Euler(40, -90, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, backwardPos, smoothRot);

//        movingForward = false;

//        if (transform.rotation == backwardPos)
//        {
//            movingBackward = false;
//        }
//    }

//    if (movingLeft == true && movingRight == false)
//    {
//        Quaternion leftPos = Quaternion.Euler(25, -115, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, leftPos, smoothRot);

//        movingRight = false;

//        if (transform.rotation == leftPos)
//        {
//            movingLeft = false;
//        }
//    }

//    if (movingRight == true && movingLeft == false)
//    {
//        Quaternion rightPos = Quaternion.Euler(25, -65, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, rightPos, smoothRot);

//        movingLeft = false;

//        if (transform.rotation == rightPos)
//        {
//            movingRight = false;
//        }
//    }

//    //diagonal

//    if (movingForward == true && movingLeft == true)
//    {
//        Quaternion northWestPos = Quaternion.Euler(10, -115, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, northWestPos, smoothRot);

//        if (transform.rotation == northWestPos)
//        {
//            movingForward = false;
//            movingLeft = false;
//        }
//    } // northwest

//    if (movingForward == true && movingRight == true)
//    {
//        Quaternion northEastPos = Quaternion.Euler(20, -75, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, northEastPos, smoothRot);

//        if (transform.rotation == northEastPos)
//        {
//            movingForward = false;
//            movingRight = false;
//        }
//    } // northeast

//    if (movingBackward == true && movingLeft == true)
//    {
//        Quaternion southWestPos = Quaternion.Euler(40, -100, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, southWestPos, smoothRot);

//        if (transform.rotation == southWestPos)
//        {
//            movingBackward = false;
//            movingLeft = false;
//        }
//    } // southwest

//    if (movingBackward == true && movingRight == true)
//    {
//        Quaternion southEastPos = Quaternion.Euler(35, -60, 0);

//        transform.rotation = Quaternion.Lerp(transform.rotation, southEastPos, smoothRot);

//        if (transform.rotation == southEastPos)
//        {
//            movingBackward = false;
//            movingRight = false;
//        }
//    } // southeast
//}

//void calculateCameraPos()
//{
//    calculatePosBool = true;

//    sc.transform.position = new Vector3(target.position.x, target.position.y + 15, target.position.z);



//}