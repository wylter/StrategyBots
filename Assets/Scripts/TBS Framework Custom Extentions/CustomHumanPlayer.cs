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

        GameObject cellGridObject = GameObject.Find("CellGrid");
        if (cellGridObject != null) {
            CellGrid cellGrid = cellGridObject.GetComponent<CellGrid>();
            if (cellGrid != null) {
                cellGrid.UnitAdded += OnUnitAdded;
            } else {
                Debug.LogError("Cellgrid object doesnt have a \"CellGrid\" script attached");
            }
        } else {
            Debug.LogError("Cellgrid object not found");
        }
        
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
