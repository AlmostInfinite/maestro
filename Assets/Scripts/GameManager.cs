using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class GameManager : MonoBehaviour {

    // TODO - Stores game information(score, level info, etc) - loads/saves game info?? - Single Instance

    public static GameManager instance = null;
    private LevelMapper levelMapper;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        levelMapper = GetComponent<LevelMapper>(); // boardScript - Remove Comment_

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    public void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);

    }


    public void Test()
    {
        Debug.Log("Woot");
    }

}
