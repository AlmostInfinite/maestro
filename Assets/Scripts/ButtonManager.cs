using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadLevel(string NewGamelvl)
    {
        SceneManager.LoadScene(NewGamelvl);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
