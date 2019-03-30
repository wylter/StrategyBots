using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _heal = 0;

    private CustomUnit _targetUnit = null;

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        base.EnterState(cellGrid, unit);
        cellGrid.CellGridState = new CellGridStateUnityAbilityTileTarget(cellGrid, unit);
    }

    public override void OnCellSelected(Cell cell, List<Cell> gridCells) {
        Debug.LogError("Not usuable option called");
    }

    public override void OnUnitSelected(CustomUnit unit) {
        unit.abilityActionUsable = false;
        _unit.isActing = true;
        _unit.animator.SetTrigger("Ability");

        _targetUnit = unit;
    }

    public override void Use() {
        Debug.Log("HEal");

        _unit.isActing = false;
    }
}
