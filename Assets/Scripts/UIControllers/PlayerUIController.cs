using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour{

    [SerializeField]
    private int playerNumber = 0;
    public int PlayerNumber { get { return playerNumber; } }

    [Space]
    [Header("Buttons")]
    [SerializeField]
    private Button _moveButton = null;
    [SerializeField]
    private Button _attackButton = null;
    [SerializeField]
    private Button _abilityButton = null;
    [SerializeField]
    private Button _skipButton = null;
    [Space]
    [SerializeField]
    private TextMeshProUGUI _abilityCostText = null;
    [SerializeField]
    private TextMeshProUGUI _gameoverText = null;

    private GameController _gameController;
    private Animator _animator;

    private void Start() {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _animator = GetComponent<Animator>();
        Debug.Assert(_gameController != null, "GameController not found");
        Debug.Assert(_animator != null, "Animator not found");
        Debug.Assert(_moveButton != null, "MoveButton is null");
        Debug.Assert(_attackButton != null, "AttackButton is null");
        Debug.Assert(_abilityButton != null, "AbilityButton is null");
        Debug.Assert(_skipButton != null, "SkipButton is null");
        Debug.Assert(_abilityCostText != null, "AbilityCostText is null");
        Debug.Assert(_gameoverText != null, "GameoverText is null");
    }

    //Changes all buttons interactability
    public void SetButtonsInteractable(bool enabled) {
        _moveButton.interactable = enabled;
        _attackButton.interactable = enabled;
        _abilityButton.interactable = enabled;
        _skipButton.interactable = enabled;
    }

    //Notfy the GameController on what action the player wants to take by pressing a specified button
    public void NotifyPlayerInput(int action) {
        _gameController.NotifyPlayerSelectedAction((PlayerSelectedAction) action);
    }

    //Displays the cost of the current ability
    public void SetAbilityCost(int cost) {
        _abilityCostText.SetText(cost.ToString());
    }

    //Initialize Gameover text and starts the show animation
    public void ShowGameoverText(string text, Color color) {
        _gameoverText.SetText(text);
        _gameoverText.color = color;
        _animator.SetTrigger("Gameover");
    }

}
