using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour{

    private Animator _animator;
    private GameController _gameController;

    private void Start() {
        _animator = GetComponent<Animator>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    //Toogles the pause panel
    public void ToglePause(bool toggle) {
        _animator.SetBool("Pause", toggle);
    }
    //Notfy the GameController on what action the player wants to take by pressing a specified button
    public void NotifyPlayerInput(int action) {
        _gameController.NotifyMenuSelectedAction((MenuSelectedAction)action);
    }

}
