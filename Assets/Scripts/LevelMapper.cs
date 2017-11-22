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

    public static LevelMapper instance = null;

    public GameObject spawnPosition;
    public GameObject spawnUnit;
	public GameObject backWall, leftWall, rightWall;


    // Array to store tile types
    public TileType[] tileTypes;

    int[,] tiles;           //2 dimentional int array to store the tiles x and z(y) location.
    Node[,] graph;          //2 dimentional Node array to store the tiles x and z(y) location.

    List<Node> mappedPath;  // List stored path generated for audience to take.

    //TODO saave numbers to a file and load on init.


    // Map variables

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

    // Position to start at - Set in inspector
    public int tileSourceX = 20;
    public int tileSourceZ = 8;

    // Position to travel to - Set in inspector
    public int tileTargetX = 5;
    public int tileTargetZ = 3;

    public Vector3 tileSourceXYZ;


    private void Update()
    {

        // Draw our debug line showing the pathfinding!
        // NOTE: This won't appear in the actual game view.

        if (mappedPath != null)
        {
            int currNode = 0;

            while (currNode < mappedPath.Count - 1)
            {

                Vector3 start = TileCoordToWorldCoord(mappedPath[currNode].x, mappedPath[currNode].z) +
                    new Vector3(0, 0.5f, 0);
                Vector3 end = TileCoordToWorldCoord(mappedPath[currNode + 1].x, mappedPath[currNode + 1].z) +
                    new Vector3(0, 0.5f, 0);

                Debug.DrawLine(start, end, Color.black, 0, true);

                currNode++;
            }

        }
    }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject); // TODO Enable when change the menu

    }

    private void Start()
    {
        // Calculates total map size including borders
        CalculateMapSize();

        // Create a Vector3 for the spawn point
        tileSourceXYZ = TileCoordToWorldCoord(tileSourceX, tileSourceZ);

        // Generates the level map
        GenerateMapData();

        // Instaniates the map tiles/towers in their locations
        GenerateMapVisuals();

        // Create a graph(array) that converts tiles to nodes and stores their positions and a nodes neighbor positions.
        GeneratePathfindingGraph();

        // Generate a path from start to finish.
        GeneratePathTo(tileTargetX, tileTargetZ);

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
        tiles[6 + leftBorder, 10 + bottomBorder] = 0;
        tiles[6 + leftBorder, 0 + bottomBorder] = 0;

        //Row 7
        //None

        //Row 8
        tiles[8 + leftBorder, 5 + bottomBorder] = 0;
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
					go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity,transform); //Quaternion.identity disables any rotation
                }

                // Set tile params
                Tile currentTile = go.GetComponent<Tile>();
                currentTile.tileX = x;
                currentTile.tileZ = z;
                //ct.map = this; not needed??

            }
        }
        //TODO Move into seperate function
        Instantiate(backWall, new Vector3((totalMapSizeX/2), 5, (totalMapSizeZ - 0.5f)), backWall.transform.rotation,transform);
        Instantiate(leftWall, new Vector3((0), 5, totalMapSizeZ), leftWall.transform.rotation, transform);
        Instantiate(rightWall, new Vector3(((totalMapSizeX) - 0.5f), 5, totalMapSizeZ), rightWall.transform.rotation, transform);

    }

    private void GeneratePathfindingGraph()
    {

        // TODO only create graph for walkable area using border variables

        // Initialize the array
        graph = new Node[totalMapSizeX, totalMapSizeZ];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < totalMapSizeX; x++)
        {
            for (int z = 0; z < totalMapSizeZ; z++)
            {
                graph[x, z] = new Node
                {
                    x = x,
                    z = z
                };
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < totalMapSizeX; x++)
        {
            for (int z = 0; z < totalMapSizeZ; z++)
            {

                // This is the 4-way connection version:
                if (x > 0)
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                if (x < totalMapSizeX - 1)
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < totalMapSizeZ - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);


                // This is the 8-way connection version (allows diagonal movement)
                // Try left
                //if(x > 0) {
                //	graph[x,z].neighbours.Add( graph[x-1, z] );
                //	if(z > 0)
                //		graph[x,z].neighbours.Add( graph[x-1, z-1] );
                //	if(z < totalMapSizeY-1)
                //		graph[x,z].neighbours.Add( graph[x-1, z+1] );
                //}

                //// Try Right
                //if(x < totalMapSizeX-1) {
                //	graph[x,z].neighbours.Add( graph[x+1, z] );
                //	if(z > 0)
                //		graph[x,z].neighbours.Add( graph[x+1, z-1] );
                //	if(z < totalMapSizeY-1)
                //		graph[x,z].neighbours.Add( graph[x+1, z+1] );
                //}

                //// Try straight up and down
                //if(z > 0)
                //	graph[x,z].neighbours.Add( graph[x, z-1] );
                //if(z < totalMapSizeY-1)
                //	graph[x,z].neighbours.Add( graph[x, z+1] );

                // This also works with 6-way hexes and n-way variable areas (like EU4)
            }
        }

        ////For Debugging graph data

        //if (graph.Length > 0)
        //{
        //    Debug.Log(graph.Length);
        //    int graphX = graph[0, 1].neighbours.Count; // Check how many neighbours the tile at graph[x,z] has
        //    Debug.Log(graphX);
        //}
        //else
        //{ Debug.Log(graph.Length); }

        ////End Debugging

    }

    public void GeneratePathTo(int targetX, int targetZ)
    {

        Dictionary<Node, float> distanceToNode = new Dictionary<Node, float>();
        Dictionary<Node, Node> previousNode = new Dictionary<Node, Node>();

        // Setup the que -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            tileSourceX,
                            tileSourceZ
                            ];

        Node target = graph[
                            targetX, //use spublic values
                            targetZ
                            ];

        distanceToNode[source] = 0;
        previousNode[source] = null;

        //    // Initialize everything to have INFINITY distance,
        //    // some nodes CAN'T be reached from the source,
        //    // which would make INFINITY a reasonable value.

        foreach (Node v in graph) // v represents each unvisited vertex in graph
        {
            if (v != source)
            {
                distanceToNode[v] = Mathf.Infinity;
                previousNode[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "closestV" is going to be the unvisited node with the smallest distance.
            Node closestV = null;

            foreach (Node possibleClosestV in unvisited)
            {
                if (closestV == null || distanceToNode[possibleClosestV] < distanceToNode[closestV])
                {
                    closestV = possibleClosestV;
                }
            }

            if (closestV == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(closestV);

            foreach (Node v in closestV.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = distanceToNode[closestV] + CostToEnterTile(closestV.x, closestV.z, v.x, v.z);
                if (alt < distanceToNode[v])
                {
                    distanceToNode[v] = alt;
                    previousNode[v] = closestV;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (previousNode[target] == null)
        {
            // No route between our target and the source
            Debug.Log("Error No Route Found");
            return;
        }

        mappedPath = new List<Node>();

        Node curr = target;

        // Step through the "previousNode" chain and add it to our path
        while (curr != null)
        {
            mappedPath.Add(curr);
            curr = previousNode[curr];
        }

        // Right now, mappedPath describes a route from out target to our source
        // Invert it!

        mappedPath.Reverse();

    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;
    }

    private bool UnitCanEnterTile(int x, int y)
    {
        return tileTypes[tiles[x, y]].isWalkable;
    }

    public void GetMappedPath(GameObject audienceUnit)
    {
        // Sets audience members current path to the original mapped path.
        audienceUnit.GetComponent<AudienceUnit>().currentPath = mappedPath;

    }

    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

}

