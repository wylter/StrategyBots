using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehavior : MonoBehaviour{

    public int range;
    protected CustomUnit _unit;

    public abstract void Use();
    public abstract void OnCellSelected(Cell cell, List<Cell> gridCells);

    public virtual void EnterState(CellGrid cellGrid, CustomUnit unit) {
        _unit = unit;
    }

    public virtual IEnumerator ResolveAbility() {
        while (_unit.isActing) {
            yield return null;
        }

        _unit.Defend(_unit, _unit.GetAbilityCost());

        Use();
    }
}
