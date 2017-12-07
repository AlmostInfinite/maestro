using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class LevelMapper : MonoBehaviour
{

    [System.Serializable]
    public class InstrumentTypeInfo
    {

        public string name;
        public Color instrumentColor;

    }

    public InstrumentTypeInfo[] instrumentTypes;

    public List<GameObject> emptySeats = new List<GameObject>();

    public static LevelMapper instance = null;

    [HideInInspector]
    public GameObject spawnPosition, spawnUnit, backWall, leftWall, rightWall;

    // Array to store tile types
    public TileType[] tileTypes;


    public int[,] tilesref; //2 dimentional int array to store the tiles x and z(y) location.
    Node[,] graph;          //2 dimentional Node array to store the tiles x and z(y) location.

    List<Node> mappedPath;  // List stored path generated for audience to take.

    // Map variables

    // Map size including borders
    [HideInInspector]
    public int totalMapSizeX, totalMapSizeZ;

    // Position to start at - Recieved from level class
    [HideInInspector]
    public int tileSourceX, tileSourceZ;
    [HideInInspector]
    public Vector3 tileSourceXYZ;

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
        DontDestroyOnLoad(gameObject); // TODO Enable when change the menu


    }


    private void Start()
    {
        
    }

    private void Update()
    {


        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        ////Physics.Raycast(ray, out hit, 100);
        //Vector3 hitOffset = new Vector3(0, -0.5f, 0);
        //if (Physics.Raycast(ray, out hit, 200))
        //    {
        //    Debug.DrawLine(ray.origin, hit.point - hitOffset, Color.red);
        //        //if (hit.collider.gameObject.CompareTag("Interactable")) //and not current object
        //        //print(hit.collider.gameObject.name);
        //        // set current object to what you are looking at
        //        // else set current object found to nothing
        //    }


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


    // Create a Vector3 for the spawn point
    public void SetTileSource(int sourceX, int sourceZ)
    {
        tileSourceX = sourceX;
        tileSourceZ = sourceZ;
        tileSourceXYZ = TileCoordToWorldCoord(tileSourceX, tileSourceZ);
    }


    public void GenerateMapVisuals()
    {

        // Find the level game object for instanciating under the parent
        var levelObject = GameObject.Find("Level").transform;

        for (int x = 0; x < totalMapSizeX; x++)
        {
            for (int z = 0; z < totalMapSizeZ; z++)
            {
                TileType currentTileType = tileTypes[tilesref[x, z]];

                GameObject go;

                if (currentTileType.isTower) // check if tower and set alignment.
                {

                    float xAdj = x + 0.5f; // used to centre tower tiles in grid
                    float yAdj = 0.5f; // moves tower on top of tiles(tower model determine this height)

                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(xAdj, yAdj, z), currentTileType.tileVisualPrefab.transform.rotation, levelObject); //Rotation of prefab
                }
                else
                {
                    go = (GameObject)Instantiate(currentTileType.tileVisualPrefab, new Vector3(x, 0, z), currentTileType.tileVisualPrefab.transform.rotation, levelObject); //Quaternion.identity disables any rotation
                }
                // Set tile params
                Tile currentTile = go.GetComponent<Tile>();
                currentTile.tileX = x;
                currentTile.tileZ = z;
                //ct.map = this; not needed??

            }
        }

        // TODO FIX TEXTURE
        //backWall.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_MainTex", Resources.Load("Sprites/Walls") as Texture);

        //TODO Move into seperate function and define in level class so different for each level
        // Generate Walls
        Instantiate(backWall, new Vector3((totalMapSizeX / 2), 5, (totalMapSizeZ - 0.5f)), backWall.transform.rotation, levelObject);
        Instantiate(leftWall, new Vector3((0), 5, (totalMapSizeZ / 2)), leftWall.transform.rotation, levelObject);
        Instantiate(rightWall, new Vector3(((totalMapSizeX) - 0.5f), 5, (totalMapSizeZ / 2)), rightWall.transform.rotation, levelObject);

    }


    public void GenerateSeatMap()
    {

        //if (emptySeats != null)
        //{
            emptySeats.Clear();
            foreach (GameObject seat in GameObject.FindGameObjectsWithTag("Seat"))
            {
                emptySeats.Add(seat);
            }
        //}
    }


    public Vector3 GetRandomSeatPos()
    {
       // if (emptySeats.Count > 0)
        //{
         int rand = UnityEngine.Random.Range(0, emptySeats.Count);

        Vector3 seatPos = emptySeats[rand].transform.position;

        emptySeats.RemoveAt(rand);

        return seatPos;

        //}
   
       // return null;
    }


    public void GeneratePathfindingGraph()
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
        TileType tt = tileTypes[tilesref[targetX, targetY]];

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
        return tileTypes[tilesref[x, y]].isWalkable;
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

