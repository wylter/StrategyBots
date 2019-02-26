using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellGridStateUnitAttack : CellGridState {

    private CustomUnit _unit;
    private List<Cell> _attackableCellsInRange;

    private Cell _unitCell;


    public CellGridStateUnitAttack(CellGrid cellGrid, Unit unit) : base(cellGrid) {
        _unit = (CustomUnit)unit;
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
            cell.UnMark();
        }
    }
}
