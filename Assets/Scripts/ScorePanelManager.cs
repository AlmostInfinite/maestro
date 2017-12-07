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

    public GameObject PanelOne;
    public GameObject PanelTwo;
    public GameObject PanelThree;

	
	// Update is called once per frame
	void Update ()
    {
		if (Onestar)
        {
            ShowPanelOne();
        }

        if (TwoStar)
        {
            ShowPanelTwo();
        }

        if (ThreeStar)
        {
            ShowPanelThree();
        }
	}


    public void ShowPanelOne ()
    {
        PanelOne.SetActive(true);
    }

    public void ShowPanelTwo()
    {  
        PanelTwo.SetActive(true);
    }

    public void ShowPanelThree()
    {
        PanelThree.SetActive(true);
    }

    public void ReturnToMenu(string LevelToLoad)
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
