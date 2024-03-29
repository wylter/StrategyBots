﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnitAttack : CellGridState {

    private CustomUnit _unit;
    private List<Cell> _attackableCellsInRange;

    private Cell _unitCell;


    public CellGridStateUnitAttack(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = unit as CustomUnit;
        _attackableCellsInRange = new List<Cell>();
    }

    public override void OnCellClicked(Cell cell) {

    }

    public override void OnCellDeselected(Cell cell) {

    }

    public override void OnCellSelected(Cell cell) {

    }

    public override void OnStateEnter() {
        base.OnStateEnter();

        _unit.OnUnitSelected();
        _unitCell = _unit.Cell;


        _attackableCellsInRange = _unit.GetAvailableAttackableCells(_cellGrid.Cells);

        foreach (CustomSquare cell in _attackableCellsInRange) {
            cell.MarkAsAttackable();
        }
    }

    public override void OnStateExit() {
        _unit.OnUnitDeselected();

        foreach (var cell in _cellGrid.Cells) {
            if (_unit.Cell != cell) {
                cell.UnMark();
            }
        }
    }

    public override void OnUnitClicked(Unit unit) {

        if (unit.Equals(_unit) || _unit.isMoving)
            return;

        if (_attackableCellsInRange.Contains(unit.Cell) && unit.PlayerNumber != _unit.PlayerNumber && _unit.ActionPoints > 0) {
            _unit.DealDamage(unit);
            _cellGrid.CellGridState = new CellGridStateWaitingForUnitInput(_cellGrid, _unit);
        }
    }
}
