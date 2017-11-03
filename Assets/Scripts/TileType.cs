using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores details about tile types.
/// </summary>

[System.Serializable]
public class TileType {

    public string name;
    public GameObject tileVisualPrefab;

    public bool isTower = false;
    public bool isWalkable = true;
    public float movementCost = 1; //TODO Remove movement cost from pathing script?

}
