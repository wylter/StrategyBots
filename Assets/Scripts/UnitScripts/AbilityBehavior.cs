using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehavior : MonoBehaviour{

    public int range;

    public abstract void Use();
    public abstract void EnterState(CellGrid cellGrid, CustomUnit unit);
}
