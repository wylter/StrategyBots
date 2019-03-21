﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _damage;

    private CustomUnit _unit;

    private List<Cell> targetSquares;

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        _unit = unit;
        cellGrid.CellGridState = new CellGridStateUnityAbilityTileTarget(cellGrid, unit);
    }

    public override void OnCellSelected(Cell cell, List<Cell> gridCells) {

        _unit.abilityActionUsable = false;
        _unit.isActing = true;
        _unit.animator.SetTrigger("Ability");
        StartCoroutine(ResolveAbility());

        CustomSquare square = cell as CustomSquare;
        Debug.Log(square.name);

        targetSquares = square.GetNeighbours(gridCells);
        targetSquares.Add(cell);
    }

    public override IEnumerator ResolveAbility() {
        while (_unit.isActing) {
            yield return null;
        }

        foreach (CustomSquare targetSquare in targetSquares) {
            if (targetSquare.unit && targetSquare.unit.PlayerNumber != _unit.PlayerNumber) {
                targetSquare.unit.Defend(_unit, _damage);
            }
        }
    }

    public override void Use() {
        
    }
}