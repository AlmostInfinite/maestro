using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int playerScore;

    public ScorePanelManager panel; 

    void start ()
    {
        panel = GetComponent<ScorePanelManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (playerScore <= 20)
        {
            panel.Onestar = true;
        }

        if (playerScore <=35)
        {
            panel.TwoStar = true;
        }

        if(playerScore <= 50)
        {
            panel.ThreeStar = true; 
        }
	}
}
