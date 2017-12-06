using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : Level
{
    // TODO - loads/saves level layout - save map variables to a file and load on init.



    private void Start()
    {

        // Calculates total map size including borders
        CalculateMapSize();

        // Create a Vector3 for the spawn point and store in the mapper
        LevelMapper.instance.SetTileSource(tileSourceX, tileSourceZ);

        // Generates the level map
        GenerateMapData();

        // Hack
        LevelMapper.instance.tilesref = tiles;

        // Instaniates the map tiles/towers in their locations
        LevelMapper.instance.GenerateMapVisuals();

        // Create a graph(array) that converts tiles to nodes and stores their positions and a nodes neighbor positions.
        LevelMapper.instance.GeneratePathfindingGraph();

        // Generate a path from start to finish.
        LevelMapper.instance.GeneratePathTo(tileTargetX, tileTargetZ);


    }

    private void CalculateMapSize()
    {
        totalMapSizeX = mapSizeX + leftBorder + rightBorder;
        totalMapSizeZ = mapSizeZ + topBorder + bottomBorder;
        LevelMapper.instance.totalMapSizeX = totalMapSizeX;
        LevelMapper.instance.totalMapSizeZ = totalMapSizeZ;
    }

    private void GenerateMapData()
    {
        tiles = null;

        // Initiate our tiles array size

        tiles = new int[totalMapSizeX, totalMapSizeZ];

        GenerateWalkableArea();

        GenerateBorderArea();

        GenerateTowerNodes();

        GenerateSeating();

        GenerateBlockers();

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

    override public void GenerateBorderArea()
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

    override public void GenerateSeating()
    {
        //TODO Fix by creating a loop that imports from a file and stores seat posistion in variables 

        //*********************************************************************
        //
        // EXAMPLE tiles[ROWNUMBER + Border, COLUMNNUMBER + Border] = TILETYPE;
        // TILETYPE 1 = SEAT
        // TILETYPE 2 = BLOCKER
        //
        //*********************************************************************

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

    override public void GenerateBlockers()
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

    override public void GenerateTowerNodes()
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

}
