using System;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour{

    public CellGrid CellGrid;
    [SerializeField]
    private List<PlayerUIController> uiController;

    void Awake(){ 
        CellGrid.LevelLoading += onLevelLoading;
        CellGrid.LevelLoadingDone += onLevelLoadingDone;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameStarted += OnGameStarted;
    }

    private void Start() {
        Debug.Assert(uiController.Count == 2, "There should be one UI for each of the 2 players");
    }

    private void onLevelLoading(object sender, EventArgs e){
        Debug.Log("Level is loading");
    }

    private void onLevelLoadingDone(object sender, EventArgs e){
        Debug.Log("Level loading done");
    }

    private void OnTurnEnded(object sender, EventArgs e) {
        uiController.ForEach(ui => ui.SetButtonsInteractable(ui.PlayerNumber == CellGrid.CurrentPlayerNumber));
    }

    private void OnGameStarted(object sender, EventArgs e) {
        uiController.ForEach(ui => ui.SetButtonsInteractable(ui.PlayerNumber == CellGrid.CurrentPlayerNumber));
    }

    public void NotifyEndTurn() {
        CellGrid.EndTurn();
    }
}
