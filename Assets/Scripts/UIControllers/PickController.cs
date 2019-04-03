using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickController : MonoBehaviour{

    [SerializeField]
    private int battlegroundLevelIndex = 1;
    [SerializeField]
    private DataController _data = null;
    [SerializeField]
    private Settings _settings = null;


    private LevelLoader _loader = null;

    private int[] _playerPicksNumber;
    public int[]  playerPicksNumber { get { return _playerPicksNumber; } }

    private void Awake() {
        _playerPicksNumber = new int[2];
    }

    private void Start() {
        _loader = GameObject.FindGameObjectWithTag("Utility")?.GetComponent<GameUtilitiesManager>()?.levelLoader;
        Debug.Assert(_data != null, "DataController is null");
        Debug.Assert(_settings != null, "Settings is null");
        Debug.Assert(_loader != null, "LevelLoader not found");
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
        if (playerPicksNumber[0] == _settings.numberOfUnits && playerPicksNumber[1] == _settings.numberOfUnits) {
            _loader.FadeToLevel(battlegroundLevelIndex);
        } else {
            Debug.Log("Player Didnt finish pick fase");
        }
    }
}
