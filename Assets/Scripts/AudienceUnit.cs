using System.Collections;
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
    public LevelMapper levelMapper;

    // tileX and tileY represent the correct map-tile position
    // for this piece.  Note that this doesn't necessarily mean
    // the world-space coordinates, because our map might be scaled
    // or offset or something of that nature.  Also, during movement
    // animations, we are going to be somewhere in between tiles.
    public int tileX;
    public int tileZ;

    // stores the current position on the path.
    private int currentPathNode = -1;

    //float remainingMovement = 100;
    public float moveSpeed = 4.0f; //How fast player moves, Increase to increase speed.

    bool move = true;

    private void Awake()
    {

    }

    private void Start()
    {

        levelMapper = FindObjectOfType<LevelMapper>();

        levelMapper.GetMappedPath(gameObject);

        tileX = levelMapper.tileSourceX;
        tileZ = levelMapper.tileSourceZ;

    }

    void Update()
    {

        //TODO Move movement into movement controller.

        //// Have we moved our visible piece close enough to the target tile that we can
        //// advance to the next step in our pathfinding?
        if (Vector3.Distance(transform.position, levelMapper.TileCoordToWorldCoord(tileX, tileZ)) < 0.1f)
            AdvancePathing();

        // Smoothly animate towards the correct map tile.
        transform.position = Vector3.Lerp(transform.position, levelMapper.TileCoordToWorldCoord(tileX, tileZ), moveSpeed * Time.deltaTime);

    }

    // Advances our pathfinding progress by one tile.
    void AdvancePathing()
    {

        if (move == false)
        {
            FinshedPath();
            return;
        }

        currentPathNode++;

        // Teleport us to our correct "current" position, in case we
        // haven't finished the animation yet.
        transform.position = levelMapper.TileCoordToWorldCoord(tileX, tileZ);

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
        Destroy(gameObject); // TODO Make Better . End of path reached
    }

}
