using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    private Label label;
    private Button button;

    public float test1 = 0;


    // Start is called before the first frame update
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        
        Debug.Log("OnEnable");
        button = rootVisualElement.Q<Button>("test-button");

        button.RegisterCallback<ClickEvent>(ev => testCounter());
    }

    public void testCounter()
    {
        test1++;
        Debug.Log(test1);

        if (test1 == 3)
        {
            disableMenu();
        }
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
