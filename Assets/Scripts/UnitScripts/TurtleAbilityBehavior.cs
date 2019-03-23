﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _damage;
    [SerializeField]
    private GameObject _explosion;

    private List<Cell> targetSquares;

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        base.EnterState(cellGrid, unit);
        cellGrid.CellGridState = new CellGridStateUnityAbilityTileTarget(cellGrid, unit);
    }

    public override void OnCellSelected(Cell cell, List<Cell> gridCells) {

        _unit.abilityActionUsable = false;
        _unit.isActing = true;
        _unit.animator.SetTrigger("Ability");
        StartCoroutine(ResolveAbility());

        CustomSquare square = cell as CustomSquare;

        targetSquares = square.GetNeighbours(gridCells);
        targetSquares.Add(cell);
    }

    public override void Use() {
        foreach (CustomSquare targetSquare in targetSquares) {
            Instantiate(_explosion, targetSquare.transform.position, Quaternion.identity);
            if (targetSquare.unit && targetSquare.unit.PlayerNumber != _unit.PlayerNumber) {
                targetSquare.unit.Defend(_unit, _damage);
            }
        }
    }
}
