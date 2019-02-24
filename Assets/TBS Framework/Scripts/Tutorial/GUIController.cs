using System;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public CellGrid CellGrid;

    private Queue<Unit>[] unitsForPlayer;

    void Awake(){ 
        CellGrid.LevelLoading += onLevelLoading;
        CellGrid.LevelLoadingDone += onLevelLoadingDone;
        CellGrid.UnitAdded += OnUnitAdded;

        unitsForPlayer = new Queue<Unit>[2];
        for (int i = 0; i < unitsForPlayer.Length; i++) {
            unitsForPlayer[i] = new Queue<Unit>();
        }
    }

    private void Start() {
        
    }

    private void onLevelLoading(object sender, EventArgs e){
        Debug.Log("Level is loading");
    }

    private void onLevelLoadingDone(object sender, EventArgs e){
        Debug.Log("Level loading done");
    }

    private void OnUnitAdded(object sender, UnitCreatedEventArgs e) {
        RegisterUnit(e.unit.GetComponent<Unit>());
    }

    private void RegisterUnit(Unit unit) {
        unitsForPlayer[unit.PlayerNumber].Enqueue(unit);
    }

    public void NotifyEndTurn() {
        CellGrid.EndTurn();
    }
}
