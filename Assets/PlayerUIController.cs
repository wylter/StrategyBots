using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    

    public void SetButtonsInteractable(bool enabled) {
        _moveButton.interactable = enabled;
        _attackButton.interactable = enabled;
        _abilityButton.interactable = enabled;
        _skipButton.interactable = enabled;
    }
}
