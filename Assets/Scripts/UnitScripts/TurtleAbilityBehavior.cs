using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAbilityBehavior : AbilityBehavior {

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        cellGrid.CellGridState = new CellGridStateUnityAbilityTileTarget(cellGrid, unit);
    }

    public override void Use() {
        
    }
}
