﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombladeAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _damage  = 0;

    private List<Cell> targetSquares;

    private void Start() {
    }

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        base.EnterState(cellGrid, unit);
        cellGrid.CellGridState = new CellGridStateUnityAbilityTileTarget(cellGrid, unit);
    }

    public override void OnCellSelected(Cell cell, List<Cell> gridCells) {

        _unit.abilityActionUsable = false;
        _unit.isActing = true;
        _unit.animator.SetTrigger("Ability");

        CustomSquare square = cell as CustomSquare;

        targetSquares = square.GetNeighbours(gridCells);
        targetSquares.Add(cell);
    }

    public override void Use() {
        foreach (CustomSquare targetSquare in targetSquares) {
            if (targetSquare.unit && targetSquare.unit.PlayerNumber != _unit.PlayerNumber) {
                targetSquare.unit.Defend(_unit, _damage);
            }
        }

        _unit.isActing = false;
    }

    public override void OnUnitSelected(CustomUnit unit) {
        Debug.LogError("Not usuable option called");
    }
}