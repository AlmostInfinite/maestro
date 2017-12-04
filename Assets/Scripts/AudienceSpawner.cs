using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudienceSpawner : MonoBehaviour
{

    //TODO - Spawn audience units. - Store/access?? (from level/gamemanager) spawn timers/waves/difficulty?? - load/save settings for spawn timing, waves?? 

   public Animator conductorAnimator;

    public GameObject unitToSpawn;
    //public LevelMapper levelMapper;

    [System.Serializable]
    public class UnitWaveInfo
    {
        public GameObject[] unitsInWave;
        public int minUnits = 1, maxUnits = 1;
        public float timeBetweenSpawns = 1;

        public int NumberInThisWave()
        {
            return Random.Range(minUnits, maxUnits);
        }

        public GameObject RandomPrefab()
        {
            return unitsInWave[Random.Range(0, unitsInWave.Length)];
        }
    }


    public int currentWave;

    public bool spawnEnabled;
    public float timeBetweenWaves;

    private float waitTimeForWave;
    private bool spawning = false;
    private int lastWave = 0;

    public UnitWaveInfo[] waves;

    protected GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        waitTimeForWave = 3;



    }

    void Update()
    {

        var units = GameObject.FindGameObjectsWithTag("Enemy");

        if (units.Length == 0 & !spawning)
        {

            if (waitTimeForWave == timeBetweenWaves & spawnEnabled)
            {
                //GameManager.instance.UpdateWaveInfoText("Wave " + (lastWave) + " Complete!"); // Only display once between waves.
            }

            waitTimeForWave -= Time.deltaTime; // Only count down if all enemies are dead.
        }

        if (waitTimeForWave <= 0 & spawnEnabled == true)
        {

            var numInThisWave = waves[currentWave].NumberInThisWave();

            //gameController.UpdateWaveText(currentWave + 1);

            StartCoroutine(SpawnWave(numInThisWave));

            waitTimeForWave = timeBetweenWaves;

        }
    }



    public IEnumerator SpawnWave(int numUnits)
    {
        //gameController.UpdateWaveInfoText("Begin Wave " + (currentWave + 1));
        spawning = true;

        for (int i = 0; i < numUnits; i++)
        {
            if (spawnEnabled)
            {

                var unitToSpawn = waves[currentWave].RandomPrefab();
                
                Instantiate(unitToSpawn, LevelMapper.instance.tileSourceXYZ, unitToSpawn.transform.rotation);

                yield return new WaitForSeconds(waves[currentWave].timeBetweenSpawns);
            }
        }

        lastWave = currentWave + 1;

        // TODO END OF LEVEL
        if (currentWave >= (waves.Length - 1))
        {
            currentWave = (waves.Length - 1);
        }
        else
        {
            currentWave++;
        }

        Debug.Log("Spawning Finished");

        spawning = false;

    }


    public void SpawnWave()
    {

        if (spawnEnabled)
        {
            spawnEnabled = false;
            AudioManager.instance.musicSource.Stop();
            conductorAnimator.SetBool("Animated", false);
        }
        else
        {
            spawnEnabled = true;
            AudioManager.instance.musicSource.Play();
            conductorAnimator.SetBool("Animated", true);
        }

    }


    public void SpawnUnit()
    {

        Instantiate(unitToSpawn, LevelMapper.instance.tileSourceXYZ, unitToSpawn.transform.rotation);

    }
}
