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

        // Instaniates the map tiles/towers in their locations
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

                if (currentTileType.isTower) // check if tower and set alignment.
                {

                    float xAdj = x + 0.5f; // used to centre tower tiles in grid
                    float yAdj = 0.0f; // moves tower on top of tiles(tower scales determine this height)

                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(xAdj, yAdj, z), currentTileType.tileVisualPrefab.transform.rotation); //Rotation of prefab
                }
                else
                {
                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity); //Quaternion.identity disables any rotation
                }

                // Set tile params
                Tile currentTile = go.GetComponent<Tile>();
                currentTile.tileX = x;
                currentTile.tileZ = z;
                //ct.map = this; not needed??

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
        // Initiate our tiles array size
        tiles = new int[totalMapSizeX, totalMapSizeZ];

        GenerateWalkableArea();

        GenerateBorderArea();

        GenerateTowerNodes();

        GenerateSeating();

        GenerateBlockers();

    }

    private void GenerateBlockers()
    {

        //TODO Fix by creating a loop that imports from a file and stores seat posistion in variables 
        // eg.

        // type = seat (create ENUM for tile types)
        // Read file to get towers amount
        // loop while seats amount > 0
        // read line -> posX
        // read line -> posZ
        // tiles[posX, posZ] = type;
        // loop

        //Hack

        //Row Array starts at 0 (First row)

        //Row 0
        //None

        //Row 1
        //None

        //Row 2
        tiles[2 + leftBorder, 0 + bottomBorder] = 2;

        //Row 3
        //None

        //Row 4
        tiles[4 + leftBorder, 10 + bottomBorder] = 2;
        tiles[4 + leftBorder, 5 + bottomBorder] = 2;

        //Row 5
        //None

        //Row 6
        tiles[6 + leftBorder, 10 + bottomBorder] = 2;
        tiles[6 + leftBorder, 0 + bottomBorder] = 2;

        //Row 7
        //None

        //Row 8
        tiles[8 + leftBorder, 5 + bottomBorder] = 2;
        tiles[8 + leftBorder, 0 + bottomBorder] = 2;

        //Row 9
        //None

        //Row 10
        tiles[10 + leftBorder, 10 + bottomBorder] = 2;
        tiles[10 + leftBorder, 0 + bottomBorder] = 2;

        //Row 11
        //None

        //Row 12
        tiles[12 + leftBorder, 10 + bottomBorder] = 2;
        tiles[12 + leftBorder, 5 + bottomBorder] = 2;


        //Row 13
        //None

        //Row 14
        //None

        //Row 15
        //None

    }

    private void GenerateSeating()
    {
        //TODO Fix by creating a loop that imports from a file and stores seat posistion in variables 
        // eg.

        // type = seat (create ENUM for tile types)
        // Read file to get towers amount
        // loop while seats amount > 0
        // read line -> posX
        // read line -> posZ
        // tiles[posX, posZ] = type;
        // loop

        //Hack

        //Row Array starts at 0

        //Row 0
        tiles[0 + leftBorder, 9 + bottomBorder] = 1;
        tiles[0 + leftBorder, 8 + bottomBorder] = 1;
        tiles[0 + leftBorder, 7 + bottomBorder] = 1;
        tiles[0 + leftBorder, 6 + bottomBorder] = 1;
        tiles[0 + leftBorder, 5 + bottomBorder] = 1;
        tiles[0 + leftBorder, 4 + bottomBorder] = 1;
        tiles[0 + leftBorder, 3 + bottomBorder] = 1;
        tiles[0 + leftBorder, 2 + bottomBorder] = 1;
        tiles[0 + leftBorder, 1 + bottomBorder] = 1;

        //Row 1
        //None

        //Row 2
        tiles[2 + leftBorder, 9 + bottomBorder] = 1;
        tiles[2 + leftBorder, 8 + bottomBorder] = 1;
        tiles[2 + leftBorder, 7 + bottomBorder] = 1;
        tiles[2 + leftBorder, 6 + bottomBorder] = 1;
        tiles[2 + leftBorder, 4 + bottomBorder] = 1;
        tiles[2 + leftBorder, 3 + bottomBorder] = 1;
        tiles[2 + leftBorder, 2 + bottomBorder] = 1;
        tiles[2 + leftBorder, 1 + bottomBorder] = 1;

        //Row 3
        //None

        //Row 4
        tiles[4 + leftBorder, 9 + bottomBorder] = 1;
        tiles[4 + leftBorder, 8 + bottomBorder] = 1;
        tiles[4 + leftBorder, 7 + bottomBorder] = 1;
        tiles[4 + leftBorder, 6 + bottomBorder] = 1;
        tiles[4 + leftBorder, 4 + bottomBorder] = 1;
        tiles[4 + leftBorder, 3 + bottomBorder] = 1;
        tiles[4 + leftBorder, 2 + bottomBorder] = 1;
        tiles[4 + leftBorder, 1 + bottomBorder] = 1;

        //Row 5
        //None

        //Row 6
        tiles[6 + leftBorder, 9 + bottomBorder] = 1;
        tiles[6 + leftBorder, 8 + bottomBorder] = 1;
        tiles[6 + leftBorder, 7 + bottomBorder] = 1;
        tiles[6 + leftBorder, 6 + bottomBorder] = 1;
        tiles[6 + leftBorder, 4 + bottomBorder] = 1;
        tiles[6 + leftBorder, 3 + bottomBorder] = 1;
        tiles[6 + leftBorder, 2 + bottomBorder] = 1;
        tiles[6 + leftBorder, 1 + bottomBorder] = 1;

        //Row 7
        //None

        //Row 8
        tiles[8 + leftBorder, 9 + bottomBorder] = 1;
        tiles[8 + leftBorder, 8 + bottomBorder] = 1;
        tiles[8 + leftBorder, 7 + bottomBorder] = 1;
        tiles[8 + leftBorder, 6 + bottomBorder] = 1;
        tiles[8 + leftBorder, 4 + bottomBorder] = 1;
        tiles[8 + leftBorder, 3 + bottomBorder] = 1;
        tiles[8 + leftBorder, 2 + bottomBorder] = 1;
        tiles[8 + leftBorder, 1 + bottomBorder] = 1;

        //Row 9
        //None

        //Row 10
        tiles[10 + leftBorder, 9 + bottomBorder] = 1;
        tiles[10 + leftBorder, 8 + bottomBorder] = 1;
        tiles[10 + leftBorder, 7 + bottomBorder] = 1;
        tiles[10 + leftBorder, 6 + bottomBorder] = 1;
        tiles[10 + leftBorder, 4 + bottomBorder] = 1;
        tiles[10 + leftBorder, 3 + bottomBorder] = 1;
        tiles[10 + leftBorder, 2 + bottomBorder] = 1;
        tiles[10 + leftBorder, 1 + bottomBorder] = 1;

        //Row 11
        //None

        //Row 12
        tiles[12 + leftBorder, 9 + bottomBorder] = 1;
        tiles[12 + leftBorder, 8 + bottomBorder] = 1;
        tiles[12 + leftBorder, 7 + bottomBorder] = 1;
        tiles[12 + leftBorder, 6 + bottomBorder] = 1;
        tiles[12 + leftBorder, 4 + bottomBorder] = 1;
        tiles[12 + leftBorder, 3 + bottomBorder] = 1;
        tiles[12 + leftBorder, 2 + bottomBorder] = 1;
        tiles[12 + leftBorder, 1 + bottomBorder] = 1;

        //Row 13
        //None

        //Row 14
        tiles[14 + leftBorder, 9 + bottomBorder] = 1;
        tiles[14 + leftBorder, 8 + bottomBorder] = 1;
        tiles[14 + leftBorder, 7 + bottomBorder] = 1;
        tiles[14 + leftBorder, 6 + bottomBorder] = 1;
        tiles[14 + leftBorder, 4 + bottomBorder] = 1;
        tiles[14 + leftBorder, 3 + bottomBorder] = 1;
        tiles[14 + leftBorder, 2 + bottomBorder] = 1;
        tiles[14 + leftBorder, 1 + bottomBorder] = 1;

        //Row 15
        //None
    }

    private void GenerateTowerNodes()
    {

        //TODO Fix by creating a loop that imports from a file and stores node posistion and type in variables 
        // eg.

        // Read file to get towers amount
        // loop while towers amount > 0
        // read line -> posX
        // read line -> posZ
        // read line -> type
        // tiles[posX, posZ] = type;
        // loop


        //HACK

        //Tower Locations

        //Tower1
        tiles[1, 2] = 4;
        //Tower2
        tiles[1, 5] = 5;
        //Tower3
        tiles[1, 8] = 6;
        //Tower4
        tiles[1, 11] = 7;
        //Tower5
        tiles[1, 14] = 8;

        //TowerNode1
        tiles[0 + leftBorder, 12 + bottomBorder] = 9;
        //TowerNode2
        tiles[2 + leftBorder, 1] = 10;
        //TowerNode3
        tiles[4 + leftBorder, 12 + bottomBorder] = 9;
        //TowerNode4
        tiles[6 + leftBorder, 1] = 10;
        //TowerNode5
        tiles[8 + leftBorder, 12 + bottomBorder] = 9;
        //TowerNode6
        tiles[10 + leftBorder, 1] = 10;
        //TowerNode7
        tiles[12 + leftBorder, 12 + bottomBorder] = 9;
        //TowerNode8
        tiles[14 + leftBorder, 1] = 10;

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
