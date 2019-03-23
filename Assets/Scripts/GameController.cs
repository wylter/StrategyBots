using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour{

    [Header("Elements")]
    [SerializeField]
    private CellGrid _cellGrid; //Reference to the CellGrid in the scene
    public CellGrid cellGrid { get { return _cellGrid; } set { _cellGrid = value; } }
    [Header("Elements")]
    [SerializeField]
    private List<PlayerUIController> _uiController; //List of ui of the players
    [SerializeField]
    private PauseMenuController pauseMenu;

    private CustomHumanPlayer currentPlayer;
    public CustomHumanPlayer CurrentPlayer { get { return currentPlayer; } }

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
        currentPlayer = (CustomHumanPlayer)_cellGrid.CurrentPlayer;
        _uiController.ForEach(ui => {
            ui.SetButtonsInteractable(ui.PlayerNumber == _cellGrid.CurrentPlayerNumber);
            if (ui.PlayerNumber == _cellGrid.CurrentPlayerNumber) {
                CustomUnit unit = currentPlayer.PlayerUnits.Peek() as CustomUnit;
                if (unit != null) {
                    ui.SetAbilityCost(unit.GetAbilityCost());
                }
            }
        });
    }

    public void DeregisterUnitFromPlayer(Unit unit) {
        CustomHumanPlayer player = _cellGrid.Players.Find(p => p.PlayerNumber.Equals(unit.PlayerNumber)) as CustomHumanPlayer;
        Debug.Assert(player != null, "Error in retriving the player");

        player.PlayerUnits = new Queue<Unit>(player.PlayerUnits.Where(u => u != unit));

        if (player.PlayerUnits.Count == 0) {
            GameOver(unit.PlayerNumber);
        }
    }

    public void NotifyPlayerSelectedAction(PlayerSelectedAction action) {

        CustomUnit currentUnit = currentPlayer.CurrentUnit as CustomUnit;

        if (currentUnit.isActing == true) {
            Debug.Log("Cannot do actions while the unit is acting");
            return;
        }

        switch (action) {

            case PlayerSelectedAction.MOVE:
                if (currentUnit.MovementPoints > 0) {
                    _cellGrid.CellGridState = new CellGridStateUnitMove(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.ATTACK:
                if (currentUnit.ActionPoints > 0 && currentUnit.abilityActionUsable) {
                    _cellGrid.CellGridState = new CellGridStateUnitAttack(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.ABILITY:
                if (currentUnit.ActionPoints > 0 && currentUnit.abilityActionUsable) {
                    currentUnit.ability.EnterState(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.SKIPTURN:
                _cellGrid.EndTurn();
                break;

            case PlayerSelectedAction.PAUSE:
                pauseMenu.ToglePause(true);
                break;

            default:
                Debug.Assert(false, "Option not implemented detected");
                break;
        }
    }

    public void NotifyMenuSelectedAction(MenuSelectedAction action) {

        switch (action) {

            case MenuSelectedAction.PLAY:
                pauseMenu.ToglePause(false);
                break;

            case MenuSelectedAction.EXIT:
                Debug.Log("Exit to implement");
                break;

            default:
                Debug.Assert(false, "Option not implemented detected");
                break;
        }
    }

    private void GameOver(int losingPlayerNumber) {
        _cellGrid.CellGridState = new CellGridStateGameOver(_cellGrid);
        Debug.Log(losingPlayerNumber + "Has lost");
    }

}
