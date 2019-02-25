using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

    [SerializeField]
    private CellGrid _cellGrid; //Reference to the CellGrid in the scene
    [SerializeField]
    private List<PlayerUIController> _uiController; //List of ui of the players

    private CustomHumanPlayer currentPlayer;

    //Assignment of the events to the cellgrid event handlers
    void Awake(){ 
        _cellGrid.LevelLoading += onLevelLoading;
        _cellGrid.LevelLoadingDone += onLevelLoadingDone;
        _cellGrid.TurnEnded += OnTurnEnded;
        _cellGrid.GameStarted += OnGameStarted;
    }

    private void Start() {
        Debug.Assert(_uiController.Count == 2, "There should be one UI for each of the 2 players");
    }

    private void onLevelLoading(object sender, EventArgs e){
        Debug.Log("Level is loading");
    }

    private void onLevelLoadingDone(object sender, EventArgs e){
        Debug.Log("Level loading done");
    }

    //On turn ended change ui activated
    private void OnTurnEnded(object sender, EventArgs e) {
        ChangeCurrentPlayer();
    }

    //On game started activate first player ui
    private void OnGameStarted(object sender, EventArgs e) {
        ChangeCurrentPlayer();
    }

    private void ChangeCurrentPlayer() {
        _uiController.ForEach(ui => ui.SetButtonsInteractable(ui.PlayerNumber == _cellGrid.CurrentPlayerNumber));
        currentPlayer = (CustomHumanPlayer)_cellGrid.CurrentPlayer;
    }

    public void NotifyPlayerSelectedAction(PlayerSelectedAction action) {

        switch (action) {

            case PlayerSelectedAction.MOVE:
                _cellGrid.CellGridState = new CellGridStateUnitMove(_cellGrid, currentPlayer.CurrentUnit);
                break;

            case PlayerSelectedAction.ATTACK:
                Debug.Log("Attack not implemented");
                _cellGrid.CellGridState = new CellGridStateUnitAttack(_cellGrid, currentPlayer.CurrentUnit);
                break;

            case PlayerSelectedAction.ABILITY:
                Debug.Log("Ability not implemented");
                break;

            case PlayerSelectedAction.SKIPTURN:
                _cellGrid.EndTurn();
                break;

            default:
                Debug.Assert(false, "Option not implemented detected");
                break;
        }
    }
}
