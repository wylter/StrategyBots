using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamielAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _damage;

    private List<Cell> targetSquares;

    public override void EnterState(CellGrid cellGrid, CustomUnit unit) {
        base.EnterState(cellGrid, unit);
        cellGrid.CellGridState = new CellGridStateUnityAbilityStraightTileTarget(cellGrid, unit);
    }

    public override void OnCellSelected(Cell cell, List<Cell> gridCells) {

        _unit.abilityActionUsable = false;
        _unit.isActing = true;

        _unit.RotateUnitTowardPosition(cell.transform.position);

        _unit.animator.SetTrigger("Ability");
        StartCoroutine(ResolveAbility());

        CustomSquare square = cell as CustomSquare;
        Debug.Log(square.name);

        targetSquares = square.GetNeighbours(gridCells);
        targetSquares.Add(cell);
    }

    public override void Use() {
        foreach (CustomSquare targetSquare in targetSquares) {
            if (targetSquare.unit && targetSquare.unit.PlayerNumber != _unit.PlayerNumber) {
                targetSquare.unit.Defend(_unit, _damage);
            }
        }
    }
}
