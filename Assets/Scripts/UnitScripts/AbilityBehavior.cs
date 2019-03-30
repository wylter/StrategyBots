using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehavior : MonoBehaviour{

    public int range;
    protected CustomUnit _unit;

    public abstract void Use();
    public abstract void OnCellSelected(Cell cell, List<Cell> gridCells);
    public abstract void OnUnitSelected(CustomUnit unit);

    public virtual void EnterState(CellGrid cellGrid, CustomUnit unit) {
        _unit = unit;
    }

    public virtual void ResolveAbility() {
        _unit.Defend(_unit, _unit.GetAbilityCost());

        Use();
    }
}
