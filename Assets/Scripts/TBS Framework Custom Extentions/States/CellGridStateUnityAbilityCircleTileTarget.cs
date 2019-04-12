using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnityAbilityCircleTileTarget : CellGridState {

    private CustomUnit _unit;
    private List<Cell> _attackableCellsInRange;

    private Cell _unitCell;

    protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1), new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, -1)
    };


    public CellGridStateUnityAbilityCircleTileTarget(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = (CustomUnit)unit;
        _attackableCellsInRange = new List<Cell>();
    }

    public override void OnCellClicked(Cell cell) {

        if (_unit.isMoving)
            return;

        if (_attackableCellsInRange.Contains(cell) && _unit.abilityActionUsable) {
            _unit.ability.OnCellSelected(cell, _attackableCellsInRange);
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


        //_attackableCellsInRange = _unit.GetAvailableAttackableCells(_cellGrid.Cells, _unit.ability.range);
        foreach (var direction in _directions) {
            Cell neighbour = _cellGrid.Cells.Find(c => c.OffsetCoord == _unitCell.OffsetCoord + direction);
            if (neighbour == null) continue;

            _attackableCellsInRange.Add(neighbour);
        }

        foreach (CustomSquare cell in _attackableCellsInRange) {
            if (!cell.isTakenByObstacle) {
                cell.MarkAsReachableByAbility();
            }
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
        if (_unit.isMoving)
            return;

        if (_attackableCellsInRange.Contains(unit.Cell) && unit.PlayerNumber != _unit.PlayerNumber && _unit.abilityActionUsable) {
            _unit.ability.OnCellSelected(unit.Cell, _attackableCellsInRange);
            _cellGrid.CellGridState = new CellGridStateWaitingForUnitInput(_cellGrid, _unit);
        }
    }
}
