using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudienceSpawner : MonoBehaviour
{

    //TODO - Spawn audience units. - Store/access?? (from level/gamemanager) spawn timers/waves/difficulty?? - load/save settings for spawn timing, waves?? 

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
    //public float timeBetweenWaves;

    private float waitTimeForWave;
    private bool spawning = false;
    private int lastWave = 0;
    private int numInThisWave;

    public UnitWaveInfo[] waves;

    private void Start()
    {
        waitTimeForWave = 0;
        currentWave = 0;

    }

    void Update()
    {

        //var units = GameObject.FindGameObjectsWithTag("Enemy");

        //if (units.Length == 0 & !spawning)
        //{

        //    if (waitTimeForWave == timeBetweenWaves & spawnEnabled)
        //    {
        //        GameManager.instance.UpdateWaveInfoText("Wave " + (lastWave) + " Complete!"); // Only display once between waves.
        //    }

        //    waitTimeForWave -= Time.deltaTime; // Only count down if all enemies are dead.
        //}

        //if (waitTimeForWave <= 0 & spawnEnabled == true)
        //{

        //    var numInThisWave = waves[currentWave].NumberInThisWave();

        //    //gameController.UpdateWaveText(currentWave + 1);

        //    StartCoroutine(SpawnWave(numInThisWave));

        //    //waitTimeForWave = timeBetweenWaves;

        //}
    }



    public IEnumerator SpawnWave(int numUnits)
    {
        //gameController.UpdateWaveInfoText("Begin Wave " + (currentWave + 1));
        spawning = true;

        for (int i = 0; i < numUnits; i++)
        {
            if (spawnEnabled)
            {
                //var rndIndex = Random.Range(0, spawnLocations.Length);
                //Vector3 location = LevelMapper.instance.tileSourceXYZ; //spawnLocations[rndIndex].position;

                var unitToSpawn = waves[currentWave].RandomPrefab();

                Instantiate(unitToSpawn, LevelMapper.instance.tileSourceXYZ, unitToSpawn.transform.rotation);

                yield return new WaitForSeconds(waves[currentWave].timeBetweenSpawns);
            }
        }

        //lastWave = currentWave + 1;

        //if (currentWave >= (waves.Length - 1))
        //{
        //    currentWave = (waves.Length - 1);
        //}
        //else
        //{
        //    currentWave++;
        //}

        Debug.Log("Spawning Finished");

        spawning = false;
        //spawnEnabled = false;
        if (spawnEnabled)
            SpawnWave();
    }


    public void SpawnWave()
    {

        if (spawnEnabled)
        {
            spawnEnabled = false;
            AudioManager.instance.musicSource.Stop();
        }
        else
        {
            spawnEnabled = true;

            numInThisWave = waves[currentWave].NumberInThisWave();
            StartCoroutine(SpawnWave(numInThisWave));

            AudioManager.instance.musicSource.Play();
        }

        //int audienceWave = 10;
        //GameObject[] gos = new GameObject[audienceWave];

        //for (int i = 0; i < audienceWave; i++)
        //{
        //    GameObject go = Instantiate(spawnUnit, new Vector3(20, 0, 8), spawnUnit.transform.rotation);
        //                gos[i] = go;
        //    levelMapper.GetMappedPath(gos[i]);
        //}
    }


    public void SpawnUnit()
    {

        Instantiate(unitToSpawn, LevelMapper.instance.tileSourceXYZ, unitToSpawn.transform.rotation);

    }
}
