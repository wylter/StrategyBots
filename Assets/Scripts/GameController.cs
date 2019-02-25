using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

    [SerializeField]
    private CellGrid CellGrid; //Reference to the CellGrid in the scene
    [SerializeField]
    private List<PlayerUIController> uiController; //List of ui of the players

    //Assignment of the events to the cellgrid event handlers
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

    //On turn ended change ui activated
    private void OnTurnEnded(object sender, EventArgs e) {
        uiController.ForEach(ui => ui.SetButtonsInteractable(ui.PlayerNumber == CellGrid.CurrentPlayerNumber));
    }

    //On game started activate first player ui
    private void OnGameStarted(object sender, EventArgs e) {
        uiController.ForEach(ui => ui.SetButtonsInteractable(ui.PlayerNumber == CellGrid.CurrentPlayerNumber));
    }

    public void NotifyPlayerSelectedAction(PlayerSelectedAction action) {

        switch (action) {

            case PlayerSelectedAction.MOVE:
                Debug.Log("Move not implemented");
                break;

            case PlayerSelectedAction.ATTACK:
                Debug.Log("Attack not implemented");
                break;

            case PlayerSelectedAction.ABILITY:
                Debug.Log("Ability not implemented");
                break;

            case PlayerSelectedAction.SKIPTURN:
                Debug.Log("Skip Turn");
                CellGrid.EndTurn();
                break;

            default:
                Debug.Assert(false, "Out of options enumeration detected");
                break;
        }
    }
}
