using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudienceSpawner : MonoBehaviour
{

    public GameObject spawnUnit;
    public LevelMapper levelMapper;


    //TODO - Spawn audience units. - Store/access?? (from level/gamemanager) spawn timers/waves/difficulty?? - load/save settings for spawn timing, waves?? 

    private void Start()
    {


    }


    public void SpawnWave()
    {
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

        //TODO FIX Instantiate

        Instantiate(spawnUnit, levelMapper.tileSourceXYZ, spawnUnit.transform.rotation);

    }
}
