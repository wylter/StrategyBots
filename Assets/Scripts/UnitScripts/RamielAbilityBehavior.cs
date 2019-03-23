using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RamielAbilityBehavior : AbilityBehavior {

    [SerializeField]
    private int _damage;
    [SerializeField]
    private GameObject _explosion;

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

        Vector2 direction = (cell.OffsetCoord - _unit.Cell.OffsetCoord).normalized;

        targetSquares = gridCells.Where(c =>
            (c.OffsetCoord - _unit.Cell.OffsetCoord).normalized.Equals(direction)
        ).ToList<Cell>();

        targetSquares = targetSquares.OrderBy(s => (s.OffsetCoord - _unit.Cell.OffsetCoord).magnitude).ToList<Cell>();
    }

    public override void Use() {
        _unit.isActing = true;
        StartCoroutine(SpawnExplosions());
    }

    private IEnumerator SpawnExplosions() {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);

        foreach (CustomSquare targetSquare in targetSquares) {
            if (targetSquare.isTakenByObstacle)
                break;

            Instantiate(_explosion, targetSquare.transform.position, Quaternion.identity);
            if (targetSquare.unit && targetSquare.unit.PlayerNumber != _unit.PlayerNumber) {
                targetSquare.unit.Defend(_unit, _damage);
            }

            yield return waitTime;
        }

        _unit.isActing = false;
    }
}
