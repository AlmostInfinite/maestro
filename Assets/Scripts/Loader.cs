using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;          //GameManager prefab to instantiate.
    public GameObject audioManager;         //SoundManager prefab to instantiate.
    public GameObject levelMapper;          //LevelMapper prefab to instantiate.

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab
            gameManager = Instantiate(gameManager);
            gameManager.name = "GameManager"; //Set GameManager GameObject name (Remove (Clone))
        }
        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
        if (AudioManager.instance == null)
        {
            //Instantiate SoundManager prefab
            audioManager = Instantiate(audioManager);
            audioManager.name = "AudioManager"; //Set AudioManager GameObject name
        }

        if (LevelMapper.instance == null)
        {
            //Instantiate SoundManager prefab
            levelMapper = Instantiate(levelMapper);
            levelMapper.name = "LevelMapper"; //Set LevelMapper GameObject name
        }

    }


    // Use this for initialization
    void Start()
    {

        Debug.Log("Managers Loaded");

        //HACK ?? Check to see if being called from pre load scene.
        //Ensure _PreLoadScene is first in build index (0)
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)

            //Load Menu Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

}
