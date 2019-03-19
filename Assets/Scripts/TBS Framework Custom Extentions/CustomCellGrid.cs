using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomCellGrid : CellGrid{

    public bool isUnitPlaying = false;

    protected override void Initialize() {
        base.Initialize();
        GetComponent<CustomObstaclesGenerator>().SpawnObstacles();
    }
}
