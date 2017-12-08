using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudioManager : MonoBehaviour
{

    // TODO - Stores music and sounds details - Handles music and sound playback.

    public AudioSource effectsSource;
    public AudioSource musicSource;
    public static AudioManager instance = null;     //Allows other scripts to call functions from SoundManager. 

    public List<AudioClip> Songs; //Songs used for each level. Songs are assigned on the prefab.


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    public void SetMusic(AudioClip clip)
    {
        musicSource.clip = clip;

    }

}
