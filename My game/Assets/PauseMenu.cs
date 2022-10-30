using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject overlay;

    public test test;
    
    public bool isMenuOn = false;
    // Start is called before the first frame update
    void Start()
    {
        test = gameObject.GetComponent<test>();

        overlay = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Listener();

        //if (isMenuOn == true)
        //{
        //    test.enableMenu();
        //}
        //else
        //{
        //    test.disableMenu();
        //}
    }

    private void Listener()
    {  
        if (Input.GetKey(KeyCode.Escape))
        {
            togglePause();
        }
    }
    private void togglePause()
    {

        GameObject.Find("PauseMenu").GetComponent<test>().disableMenu();
        Debug.Log("test");
        //if (isMenuOn == true)
        //{
        //    isMenuOn = false;

        //}
        //else if (isMenuOn == false)
        //{
        //    isMenuOn = true;

        //}

        //if (isMenuOn == false)
        //{
        //    Time.timeScale = 1f;
        //}
        //else
        //{
        //    Time.timeScale = 0f;
        //}
    }
}
