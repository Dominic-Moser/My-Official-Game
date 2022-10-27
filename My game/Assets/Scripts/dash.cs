using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{

    float maxDelay = 0.5f;
    bool dashReady = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
    void PrepareDash(bool makeReady)
    {
        //this is where the handling happens
        CancelInvoke("CancelDash"); // on call this cancels "canceldash" and makes it so you have a period of time to press the key twice
        Invoke("CancelDash", maxDelay); // the max delay gives the time period of time the player can press the second key currently 0.5 seconds if done before the time runs out, it bypasses the invoke function and pings dash
        dashReady = true;
    }
    void CancelDash()
    {
        dashReady = false;
    }
}
//"Invoke" calls a function adter the alloted period of time.
//'CancelInvoke' just cancels the invoke - duhh
