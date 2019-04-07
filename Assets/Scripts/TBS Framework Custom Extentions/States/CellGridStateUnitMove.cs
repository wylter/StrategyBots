using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnitMove : CellGridState {

    private CustomUnit _unit;
    private HashSet<Cell> _pathsInRange;

    private Cell _unitCell;

    private List<Cell> _currentPath;

    public CellGridStateUnitMove(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = unit as CustomUnit;
        _pathsInRange = new HashSet<Cell>();
        _currentPath = new List<Cell>();
    }

    public override void OnCellClicked(Cell cell) {
        if (_unit.isMoving)
            return;
        if (cell.IsTaken || !_pathsInRange.Contains(cell)) {
            return;
        }

        var path = _unit.FindPath(_cellGrid.Cells, cell);
        _unit.Move(cell, path);
        _cellGrid.CellGridState = new CellGridStateWaitingForUnitInput(_cellGrid, _unit);
    }

    public override void OnCellDeselected(Cell cell) {
        base.OnCellDeselected(cell);
        foreach (var _cell in _currentPath) {
            if (_pathsInRange.Contains(_cell))
                _cell.MarkAsReachable();
            else
                _cell.UnMark();
        }
    }

    public override void OnCellSelected(Cell cell) {
        base.OnCellSelected(cell);
        if (!_pathsInRange.Contains(cell)) return;

        _currentPath = _unit.FindPath(_cellGrid.Cells, cell);
        foreach (var _cell in _currentPath) {
            _cell.MarkAsPath();
        }
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
        foreach (var cell in _pathsInRange) {
            cell.MarkAsReachable();
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
