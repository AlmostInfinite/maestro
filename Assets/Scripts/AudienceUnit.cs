﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AudienceUnit : MonoBehaviour
{

    // TODO - Store information about audience unit. - Handle some unit actions?? 

    public List<Node> currentPath = null;

    // Hack Create instance of levelmapper
    //public LevelMapper levelMapper;
    public Level currentLevel;

    // tileX and tileY represent the correct map-tile position
    // for this piece.  Note that this doesn't necessarily mean
    // the world-space coordinates, because our map might be scaled
    // or offset or something of that nature.  Also, during movement
    // animations, we are going to be somewhere in between tiles.
    public int tileX;
    public int tileZ;

    // stores the current position on the path.
    private int currentPathNode = -1;

    float waitTime = 2.222f;
    
    // Base Unit Stats
    public int HP; // Unit HP
    public int instrumentType; // Instrument type used to kill unit and set color.
    
    bool move = true;
    bool moved = true;

    private void Awake()
    {

    }

    private void Start()
    {

        currentLevel = FindObjectOfType<Level1>();
        if (currentLevel == null)
            currentLevel = FindObjectOfType<Level2>();
        if (currentLevel == null)
            currentLevel = FindObjectOfType<Level3>();


        LevelMapper.instance.GetMappedPath(gameObject);

        tileX = LevelMapper.instance.tileSourceX;
        tileZ = LevelMapper.instance.tileSourceZ;

        instrumentType = currentLevel.unitsToSpawn[0];

        currentLevel.unitsToSpawn.RemoveAt(0);

        Color selectedColor = LevelMapper.instance.instrumentTypes[instrumentType].instrumentColor;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers)
        {
            if (r.gameObject.CompareTag("Painted"))
            r.material.SetColor("_Color", selectedColor);
        }


    }

    void Update()
    {

        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        //TODO Move movement into movement controller.

        //// Have we moved our visible piece close enough to the target tile that we can
        //// advance to the next step in our pathfinding?
        if (Vector3.Distance(transform.position, LevelMapper.instance.TileCoordToWorldCoord(tileX, tileZ)) < 0.1f)
            AdvancePathing();
     else if (moved)
        {   //Only rotate if unit has moved
            Vector3 pos = LevelMapper.instance.TileCoordToWorldCoord(tileX, tileZ) - transform.position;
            Quaternion newRot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.9f);
            moved = !moved;
        }
        

        // Smoothly animate towards the correct map tile.
        transform.position = Vector3.Lerp(transform.position, LevelMapper.instance.TileCoordToWorldCoord(tileX, tileZ), currentLevel.moveSpeed * Time.deltaTime);

    }

    // Advances our pathfinding progress by one tile.
    void AdvancePathing()
    {

        moved = !moved;

        if (move == false)
        {
            FinshedPath();
            return;
        }

        currentPathNode++;

        // Teleport us to our correct "current" position, in case we
        // haven't finished the animation yet.
        transform.position = LevelMapper.instance.TileCoordToWorldCoord(tileX, tileZ);

        // Move to the next tile in the sequence

        tileX = currentPath[currentPathNode].x;
        tileZ = currentPath[currentPathNode].z;

        if (currentPathNode >= currentPath.Count - 1)
        {
            // We only have one tile left in the path, and that tile MUST be our ultimate
            // destination -- and we are standing on it!

            move = false;
        }

    }

    void FinshedPath()
    {

        // insert particle effect/animation here.
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Leave");
        Destroy(gameObject, 0.833f);
        //Destroy(gameObject); // TODO Make Better . End of path reached
    }

}
