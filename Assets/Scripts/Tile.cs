﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class Tile : MonoBehaviour {

    // TODO - store tile position, if vacant??

    public int tileX;
    public int tileZ;

    public LevelMapper levelMap; // Do i need this in here??

    public bool isVacant = true; // bool to check if tile is vacant


}
