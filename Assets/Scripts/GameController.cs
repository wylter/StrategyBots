using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour{

    [Header("Elements")]
    [SerializeField]
    private CustomCellGrid _cellGrid; //Reference to the CellGrid in the scene
    public CustomCellGrid cellGrid { get { return _cellGrid; } set { _cellGrid = value; } }
    [Header("UI Elements")]
    [SerializeField]
    private List<PlayerUIController> _uiController = null; //List of ui of the players
    [SerializeField]
    private PauseMenuController _pauseMenu = null;
    [SerializeField]
    private Color _victoryColor = Color.white;
    [SerializeField]
    private string _victoryMessage = "YOU WON";
    [SerializeField]
    private Color _loseColor = Color.white;
    [SerializeField]
    private string _loseMessage = "YOU LOST";
    [SerializeField]
    private Animator _backButtonAnimator = null;
    [Header("Settings")]
    [SerializeField]
    private int _mainMenuSceneIndex = 1;

    private LevelLoader _loader = null;

    private CustomHumanPlayer _currentPlayer;
    public CustomHumanPlayer CurrentPlayer { get { return _currentPlayer; } }

    //Assignment of the events to the cellgrid event handlers
    void Awake(){ 
        _cellGrid.LevelLoading += onLevelLoading;
        _cellGrid.LevelLoadingDone += onLevelLoadingDone;
        _cellGrid.TurnEnded += OnTurnEnded;
        _cellGrid.GameStarted += OnGameStarted;
    }

    private void Start() {
        _loader = GameObject.FindGameObjectWithTag("Utility")?.GetComponent<GameUtilitiesManager>()?.levelLoader;
        Debug.Assert(_loader != null, "LevelLoader not found");
        Debug.Assert(_uiController.Count == 2, "There should be one UI for each of the 2 players");
        Debug.Assert(_pauseMenu != null, "PauseMenu is null");
        Debug.Assert(_loader != null, "LevelLoader is null");
        Debug.Assert(_backButtonAnimator != null, "BackButtonAnimator is null");
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
        _currentPlayer = _cellGrid.CurrentPlayer as CustomHumanPlayer;
        _uiController.ForEach(ui => {
            ui.SetButtonsInteractable(ui.PlayerNumber == _cellGrid.CurrentPlayerNumber);
            if (ui.PlayerNumber == _cellGrid.CurrentPlayerNumber) {
                CustomUnit unit = _currentPlayer.PlayerUnits.Peek() as CustomUnit;
                if (unit != null) {
                    ui.SetAbilityCost(unit.GetAbilityCost());
                }
            }
        });
    }

    public void DeregisterUnitFromPlayer(Unit unit) {

        //Checking if the unit that has to be destroyed is the same that has the current turn, in that case the game automatically skips to the next player
        bool skipAfterDestroy = false;
        if (_currentPlayer.CurrentUnit == unit) {
            skipAfterDestroy = true;
        }

        CustomHumanPlayer player = _cellGrid.Players.Find(p => p.PlayerNumber.Equals(unit.PlayerNumber)) as CustomHumanPlayer;
        Debug.Assert(player != null, "Error in retriving the player");

        if(player != null) {
            player.PlayerUnits = new Queue<Unit>(player.PlayerUnits.Where(u => u != unit));

            if (player.PlayerUnits.Count == 0) {
                GameOver(unit.PlayerNumber);
            } else if (skipAfterDestroy) {
                CustomUnit cUnit = unit as CustomUnit;
                if (cUnit) {
                    _cellGrid.EndTurn(cUnit);
                    Debug.Log("Turnskip by destruction called by " + unit.name);
                }
            }
        }
    }

    public void NotifyPlayerSelectedAction(PlayerSelectedAction action) {

        CustomUnit currentUnit = _currentPlayer.CurrentUnit as CustomUnit;

        if (currentUnit.isActing || currentUnit.isAnimating) {
            Debug.Log("Cannot do actions while the unit is acting");
            return;
        }

        switch (action) {

            case PlayerSelectedAction.MOVE:
                if (currentUnit != null && currentUnit.MovementPoints > 0) {
                    _cellGrid.CellGridState = new CellGridStateUnitMove(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.ATTACK:
                if (currentUnit != null && currentUnit.ActionPoints > 0 && currentUnit.abilityActionUsable) {
                    _cellGrid.CellGridState = new CellGridStateUnitAttack(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.ABILITY:
                if (currentUnit != null && currentUnit.ActionPoints > 0 && currentUnit.abilityActionUsable) {
                    currentUnit.ability.EnterState(_cellGrid, currentUnit);
                }
                break;

            case PlayerSelectedAction.SKIPTURN:
                _cellGrid.EndTurn();
                break;

            case PlayerSelectedAction.PAUSE:
                _pauseMenu.ToglePause(true);
                break;

            default:
                Debug.Assert(false, "Option not implemented detected");
                break;
        }
    }

    public void NotifyMenuSelectedAction(MenuSelectedAction action) {

        switch (action) {

            case MenuSelectedAction.PLAY:
                _pauseMenu.ToglePause(false);
                break;

            case MenuSelectedAction.EXIT:
                _loader.FadeToLevel(_mainMenuSceneIndex);
                break;

            default:
                Debug.Assert(false, "Option not implemented detected");
                break;
        }
    }

    private void GameOver(int losingPlayerNumber) {
        _cellGrid.CellGridState = new CellGridStateGameOver(_cellGrid);
        Debug.Log("Gameover");
        _uiController.ForEach(ui => {
            if (ui.PlayerNumber != losingPlayerNumber) {
                ui.ShowGameoverText(_victoryMessage, _victoryColor);
            } else {
                ui.ShowGameoverText(_loseMessage, _loseColor);
            }
            ui.SetButtonsInteractable(false);
        });

        _backButtonAnimator.SetTrigger("In");
    }

}
