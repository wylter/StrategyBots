using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour{


    public PlayerSelection[] playersSelection;

    private void Awake() {
        playersSelection = new PlayerSelection[2];
        playersSelection[0] = ScriptableObject.CreateInstance<PlayerSelection>();
        playersSelection[0].playerUnits = new UnitClass[PickMenuController.maxUnits];
        playersSelection[1] = ScriptableObject.CreateInstance<PlayerSelection>();
        playersSelection[1].playerUnits = new UnitClass[PickMenuController.maxUnits];
    }

    private void Start() {
        DontDestroyOnLoad(this);
    }
}
