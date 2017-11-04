using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class LevelMapper : MonoBehaviour
{

    // TODO - Single instance - loads/saves level layout - generates level layout, level data(nodes) - generates path(or create PathMapper script)??


    public GameObject spawnPosition;
    public GameObject spawnUnit;

    // Array to store tile types
    public TileType[] tileTypes;


    int[,] tiles;           //2 dimentional int array to store the tiles x and z(y) location.
    //Node[,] graph;        //2 dimentional Node array to store the tiles x and z(y) location.


    //TODO saave numbers to a file and load on init.
    // Map vaiables

    int leftBorder = 5;     // Use to move rows right(z).
    int rightBorder = 1;
    int topBorder = 3;
    int bottomBorder = 3;   // Use to move rows up(x)

    // Specify walkable map size
    public int mapSizeX = 16;
    public int mapSizeZ = 11;

    // Map size including borders
    protected int totalMapSizeX;
    protected int totalMapSizeZ;

    // Position to travel to
    public int tileFinishX = 19;
    public int tileFinishZ = 8;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Debug.Log(this.transform.position);
    }


    private void Awake()
    {



    }

    private void Start()
    {
        // Calculates total map size including borders
        CalculateMapSize();

        // Generates the level map
        GenerateMapData();


        GenerateMapVisuals();


    }

    private void GenerateMapVisuals()
    {

        for (int x = 0; x < totalMapSizeX; x++)
        {
            for (int z = 0; z < totalMapSizeZ; z++)
            {
                TileType currentTileType = tileTypes[tiles[x, z]];

                GameObject go;

                if (currentTileType.isTower) // check if tower and move by .5
                {

                    float xAdj = x + 0.5f; // used to centre tower tiles
                    //float yAdj = z + 0.0f;

                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(xAdj, 0, z), currentTileType.tileVisualPrefab.transform.rotation); //Rotation of prefab
                }
                else
                {
                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity); //Quaternion.identity disables any rotation
                }

                // Set tile params
                Tile currentTile = go.GetComponent<Tile>();
                currentTile.tileX = x;
                currentTile.tileZ = z;
                //ct.map = this; not needed?

            }
        }
    }


    private void CalculateMapSize()
    {
        totalMapSizeX = mapSizeX + leftBorder + rightBorder; //22
        totalMapSizeZ = mapSizeZ + topBorder + bottomBorder; //17
    }

    private void GenerateMapData()
    {

        // Allocate our map tiles
        tiles = new int[totalMapSizeX, totalMapSizeZ];

        // int x, z;

        GenerateWalkableArea();

        GenerateBorderArea();



    }

    private void GenerateBorderArea()
    {

        int x, z;
        
        //Hack
        // Make a wall border area left
        for (x = 0; x < leftBorder; x++)
        {
            for (z = 0; z < totalMapSizeZ; z++)
            {
                tiles[x, z] = 3;
            }
        }

        // Make a wall border area right
        for (x = totalMapSizeX - rightBorder; x < totalMapSizeX; x++)
        {
            for (z = 0; z < totalMapSizeZ; z++)
            {
                tiles[x, z] = 3;
            }
        }


        // Make a wall border area bottom
        for (z = 0; z < bottomBorder; z++)
        {
            for (x = 0; x < totalMapSizeX; x++)
            {
                tiles[x, z] = 3;
            }
        }

        // Make a wall border area top
        for (z = totalMapSizeZ - topBorder; z < totalMapSizeZ; z++)
        {
            for (x = 0; x < totalMapSizeX; x++)
            {
                tiles[x, z] = 3;
            }
        }

    }

    private void GenerateWalkableArea()
    {

        // Initialize our walkable tiled area
        for (int x = 0; x < totalMapSizeX; x++)
        {
            for (int z = 0; z < totalMapSizeZ; z++)
            {
                tiles[x, z] = 0;
            }
        }

    }





}
