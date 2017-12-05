using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
    public GameObject CreditsPanel;
    public GameObject MenuPanel;
    public GameObject SplashPanel;

    bool credits = false;
    bool menu = false;
    bool splash = true;

    void Update()
    {
        if (credits)
        {
            if (Input.GetMouseButton(0))
            {
                ShowCredits();
            }
        }
        if (menu)
        {
            if (Input.GetMouseButton(0))
            {
                ShowMenu();
            }
        }
        if (splash)
        {
            if (Input.GetMouseButton(0))
            {
                ShowSplash();
            }
        }
    }
    public void ShowCredits()
    {
        credits = true;
        splash = false;
        menu = false; 
        CreditsPanel.SetActive(true);
        MenuPanel.SetActive(false);
        SplashPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menu = true;
        credits = false;
        splash = false;
        MenuPanel.SetActive(true);
        SplashPanel.SetActive(false);
        CreditsPanel.SetActive(false);

    }

    public void ShowSplash()
    {
        splash = true; 
        credits = false;
        menu = false;
        SplashPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        MenuPanel.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
   
}
