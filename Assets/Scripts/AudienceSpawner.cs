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

    public void SpawnUnit()
    {
    //TODO FIX Instantiate
    GameObject go = Instantiate(spawnUnit, new Vector3(20, 0, 8), spawnUnit.transform.rotation);

        levelMapper.GetMappedPath(go);

        //go.GetComponent<AudienceUnit>().currentPath = 
        //selectedUnit.GetComponent<Unit>().currentPath = currentPath;

        //selectedUnit.GetComponent<Unit>().SetMap();
    }
}
