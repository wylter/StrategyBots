using System.Collections;
using UnityEngine;

public class CellGridStateWaitingForUnitInput : CellGridState {

    public CellGridStateWaitingForUnitInput(CellGrid cellGrid, CustomUnit unit) : base(cellGrid) {

        if (unit.MovementPoints == 0 && (!unit.abilityActionUsable || unit.ActionPoints == 0)) {
            CustomCellGrid grid = cellGrid as CustomCellGrid;
            grid?.EndTurn(unit); 
        }
    }

    
}

