using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadLevel(string LeveltoLoad)
    {
        SceneManager.LoadScene(LeveltoLoad);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
}
