using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHumanPlayer : HumanPlayer{
    
    private Queue<Unit> playerUnits;
    public Queue<Unit> PlayerUnits { get { return playerUnits; } set { playerUnits = value; } }

    private Unit currentUnit;
    public Unit CurrentUnit { get { return currentUnit; } set { currentUnit = value; } }


    void Awake() {

        playerUnits = new Queue<Unit>();

        GameObject.Find("CellGrid").GetComponent<CellGrid>().UnitAdded += OnUnitAdded;
    }

    private void OnUnitAdded(object sender, UnitCreatedEventArgs e) {
        RegisterUnit(e.unit.GetComponent<Unit>());
    }

    private void RegisterUnit(Unit unit) {
        if (unit.PlayerNumber == PlayerNumber) {
            playerUnits.Enqueue(unit);
        }
    }

    public override void Play(CellGrid cellGrid) {

        currentUnit = playerUnits.Dequeue();
        playerUnits.Enqueue(currentUnit);

        cellGrid.CellGridState = new CellGridStateUnitMove(cellGrid, currentUnit);
    }
    
}
