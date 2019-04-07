using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnityAbilityStraightTileTarget : CellGridState {

    private CustomUnit _unit;
    private List<Cell> _attackableCellsInRange;

    private Cell _unitCell;


    public CellGridStateUnityAbilityStraightTileTarget(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = (CustomUnit)unit;
        _attackableCellsInRange = new List<Cell>();
    }

    public override void OnCellClicked(Cell cell) {

        if (_unit.isMoving)
            return;

        if (_attackableCellsInRange.Contains(cell) && _unit.abilityActionUsable) {
            _unit.ability.OnCellSelected(cell, _cellGrid.Cells);
            _cellGrid.CellGridState = new CellGridStateWaitingForUnitInput(_cellGrid, _unit);
        }
    }

    public override void OnCellDeselected(Cell cell) {

    }

    public override void OnCellSelected(Cell cell) {

    }

    public override void OnStateEnter() {
        base.OnStateEnter();

        _unit.OnUnitSelected();
        _unitCell = _unit.Cell;


        _attackableCellsInRange = _unit.GetAvailableAttackableCells(_cellGrid.Cells, _unit.ability.range);

        _attackableCellsInRange = _attackableCellsInRange.Where(otherCell => {
            Vector2 v = (otherCell.OffsetCoord - _unitCell.OffsetCoord);
            return v.x == 0 || v.y == 0;
        }).ToList();

        foreach (CustomSquare cell in _attackableCellsInRange) {
            if (cell.unit == null || cell.unit.PlayerNumber != _unit.PlayerNumber) {
                cell.MarkAsReachableByAbility();
            }
        }
    }

    public override void OnStateExit() {
        _unit.OnUnitDeselected();

        foreach (var cell in _cellGrid.Cells) {
            cell.UnMark();
        }
    }

    public override void OnUnitClicked(Unit unit) {
        if (_unit.isMoving)
            return;

        if (_attackableCellsInRange.Contains(unit.Cell) && unit.PlayerNumber != _unit.PlayerNumber && _unit.abilityActionUsable) {
            _unit.ability.OnCellSelected(unit.Cell, _cellGrid.Cells);
            _cellGrid.CellGridState = new CellGridStateWaitingForUnitInput(_cellGrid, _unit);
        }
    }
}
