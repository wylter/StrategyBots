using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMenuController : MonoBehaviour{

    public static readonly int maxUnits = 3;

    [SerializeField]
    private LevelLoader _loader;
    [SerializeField]
    private int battlegroundLevelIndex = 1;
    

    [SerializeField]
    private DataController _data = null;

    private int[] _playerPicksNumber;
    public int[]  playerPicksNumber { get { return _playerPicksNumber; } }

    private void Awake() {
        _playerPicksNumber = new int[2];
    }

    private void Start() {
        Debug.Assert(_data != null, "DataController is null");
        Debug.Assert(_loader != null, "LevelLoader is null");
    }

    public void NotifyPick(int playerNumber, UnitClass pick, int spot) {
        _data.playersSelection[playerNumber].playerUnits[spot] = pick;
        _playerPicksNumber[playerNumber]++;
    }

    public void NotifyChangeSpotPick(int playerNumber, UnitClass pick, int spot) {
        _data.playersSelection[playerNumber].playerUnits[spot] = pick;
    }

    public void NotifyDeletePick(int playerNumber) {
        _playerPicksNumber[playerNumber]--;
    }

    public void Play() {
        if (playerPicksNumber[0] == maxUnits && playerPicksNumber[1] == maxUnits) {
            _loader.FadeToLevel(battlegroundLevelIndex);
        } else {
            Debug.Log("Player Didnt finish pick fase");
        }
    }
}
