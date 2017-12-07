using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // TODO - loads/saves level layout - save map variables to a file and load on init.


    public List<int> unitsToSpawn;

    public GameObject backWall, leftWall, rightWall;

    public float moveSpeed; //How fast unit moves, Increase to increase speed.

    public int[,] tiles;


    //// Map variables

    public int leftBorder = 5;     // Use to move rows right(z).
    public int rightBorder = 1;
    public int topBorder = 3;
    public int bottomBorder = 3;   // Use to move rows up(x)

    // Specify walkable map size
    public int mapSizeX = 16;
    public int mapSizeZ = 11;

    // Map size including borders
    protected int totalMapSizeX;
    protected int totalMapSizeZ;

    // Position to start at - Set in inspector
    public int tileSourceX = 20;
    public int tileSourceZ = 8;

    // Position to travel to - Set in inspector
    public int tileTargetX = 5;
    public int tileTargetZ = 3;


    private void Start()
    {

        //// Calculates total map size including borders
        //CalculateMapSize();

        //// Create a Vector3 for the spawn point and store in the mapper
        //LevelMapper.instance.SetTileSource(tileSourceX, tileSourceZ);

        //// Generates the level map
        //GenerateMapData();

        //// Hack
        //LevelMapper.instance.tilesref = tiles;

        //// Instaniates the map tiles/towers in their locations
        //LevelMapper.instance.GenerateMapVisuals();

        //// Create a graph(array) that converts tiles to nodes and stores their positions and a nodes neighbor positions.
        //LevelMapper.instance.GeneratePathfindingGraph();

        //// Generate a path from start to finish.
        //LevelMapper.instance.GeneratePathTo(tileTargetX, tileTargetZ);

        //// Generate Seat List
        //LevelMapper.instance.GenerateSeatMap();

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

    virtual public void GenerateBorderArea()
    {

    }

    virtual public void GenerateSeating()
    {

    }

    virtual public void GenerateBlockers()
    {


    }

    virtual public void GenerateTowerNodes()
    {


    }

}
