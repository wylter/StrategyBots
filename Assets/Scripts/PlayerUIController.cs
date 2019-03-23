using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour{

    [SerializeField]
    private int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }

    [Space]
    [Header("Buttons")]
    [SerializeField]
    private Button _moveButton;
    [SerializeField]
    private Button _attackButton;
    [SerializeField]
    private Button _abilityButton;
    [SerializeField]
    private Button _skipButton;
    [Space]
    [SerializeField]
    private TextMeshProUGUI _abilityCostText;

    private GameController _gameController;

    private void Start() {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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

}
