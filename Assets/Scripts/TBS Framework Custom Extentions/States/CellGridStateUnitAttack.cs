using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnitAttack : CellGridState {

    private Unit _unit;
    private HashSet<Cell> _pathsInRange;

    private Cell _unitCell;

    private List<Cell> _currentPath;

    public CellGridStateUnitAttack(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = unit;
        _pathsInRange = new HashSet<Cell>();
        _currentPath = new List<Cell>();
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

        _pathsInRange = _unit.GetAvailableDestinations(_cellGrid.Cells);
        var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);

        foreach (var cell in cellsNotInRange) {
            cell.UnMark();
        }
        foreach (CustomSquare cell in _pathsInRange) {
            cell.MarkAsAttackable();
        }

        if (_unit.ActionPoints <= 0) return;

    }

    public override void OnStateExit() {
        _unit.OnUnitDeselected();

        foreach (var cell in _cellGrid.Cells) {
            cell.UnMark();
        }
    }
}
