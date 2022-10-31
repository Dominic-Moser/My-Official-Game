using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    private Label label;
    private Button customizationButton;
    private Button settingsButton;
    private Button quitGameButton;

    public float test1 = 0;


    // Start is called before the first frame update
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        
        Debug.Log("OnEnable");
        customizationButton = rootVisualElement.Q<Button>("customization-button");
        settingsButton = rootVisualElement.Q<Button>("settings-button");
        quitGameButton = rootVisualElement.Q<Button>("quitgame-button");

        customizationButton.RegisterCallback<ClickEvent>(ev => CustomizationScreen());
        settingsButton.RegisterCallback<ClickEvent>(ev => SettingsScreen());
        quitGameButton.RegisterCallback<ClickEvent>(ev => QuitGameScreen());
    }



    public void CustomizationScreen()
    {
        Debug.Log("testC");
    }

    public void SettingsScreen()
    {
        Debug.Log("testS");

    }

    public void QuitGameScreen()
    {
        Debug.Log("testQ");

    }

    public void disableMenu()
    {
        GameObject.Find("PauseMenu").SetActive(false);
    }

    public void enableMenu()
    {
        GameObject.Find("PauseMenu").SetActive(true);
    }
    // Update is called once per frame
}
