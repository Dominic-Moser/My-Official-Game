using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public test test;

    
    
    public bool isMenuOn = false;
    // Start is called before the first frame update
    void Start()
    {
        disableMenu();
        
        test = gameObject.GetComponent<test>();

    }

    // Update is called once per frame
    void Update()
    {
        Listener();

    }

    private void Listener()
    {  
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            togglePause();
        }

    }
    private void togglePause()
    {

        if(pauseMenu.activeInHierarchy)
        {
            isMenuOn = false;
            disableMenu();
            gameResume();
        }
        else if (!pauseMenu.activeInHierarchy)
        {
            isMenuOn = true;
            enableMenu();
            gamePause();
        }
    }

    public void disableMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void enableMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void gamePause()
    {
        Time.timeScale = 0;
    }
    public void gameResume()
    {
        Time.timeScale = 1;
    }
}
