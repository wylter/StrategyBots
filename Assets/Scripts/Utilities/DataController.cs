using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour{

    [SerializeField]
    private Settings _settings = null;

    public PlayerSelection[] playersSelection;

    private void Awake() {
        Debug.Assert(_settings != null, "Settings is null");
        playersSelection = new PlayerSelection[2];
        playersSelection[0] = ScriptableObject.CreateInstance<PlayerSelection>();
        playersSelection[0].playerUnits = new UnitClass[_settings.numberOfUnits];
        playersSelection[1] = ScriptableObject.CreateInstance<PlayerSelection>();
        playersSelection[1].playerUnits = new UnitClass[_settings.numberOfUnits];
    }

    private void Start() {
        DontDestroyOnLoad(this);
    }
}
