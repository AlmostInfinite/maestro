using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudienceUnit : MonoBehaviour {

    // TODO - Store information about audience unit. - Handle some unit actions?? 

    public List<Node> currentPath = null;

    // Hack Create instance of levelmapper
    //public LevelMapper levelMapper;

    private void Start()
    {

        //levelMapper = FindObjectOfType<LevelMapper>();

        //levelMapper.GetMappedPath(gameObject);

        Debug.Log(currentPath.Count);

    }

}
