using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePanelManager : MonoBehaviour
{

    //score script stuff up here

   public  bool Onestar = false;
   public  bool TwoStar = false;
   public bool ThreeStar = false;
   public bool endofLevel = false;

    

    public GameObject ImageOne;
    public GameObject ImageTwo;
    public GameObject ImageThree;
    public GameObject Panel;


    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update ()
    {
        if (endofLevel)
        {
            if (Onestar)
            {
                ShowPanelOne();
            }

            else if (TwoStar)
            {
                ShowPanelTwo();
            }

           else if (ThreeStar)
            {
                ShowPanelThree();
            }
        }
	}


    public void ShowPanelOne ()
    {
        Panel.SetActive(true);
        ImageOne.SetActive(true);
    }

    public void ShowPanelTwo()
    {
        Panel.SetActive(true);
        ImageOne.SetActive(true);
        ImageTwo.SetActive(true);
    }

    public void ShowPanelThree()
    {
        Panel.SetActive(true);
        ImageOne.SetActive(true);
        ImageTwo.SetActive(true);
        ImageThree.SetActive(true);
    }

    public void ReturnToMenu(string LevelToLoad)
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
