using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomUnitGeneratorFromData : MonoBehaviour, IUnitGenerator {

    [Header("Elements")]
    [SerializeField]
    private Transform UnitsParent;
    [SerializeField]
    private Transform CellsParent;
    [SerializeField]
    private List<Transform> _player1SpawnPoints = null;
    [SerializeField]
    private List<Transform> _player2SpawnPoints = null;
    [Space]
    [Header("Settings")]
    [SerializeField]
    private UnitClassEnumToPrefab unitClassToPrefab = null;

    private DataController _data;



    private void Start() {
        Debug.Assert(_player1SpawnPoints.Count == PickMenuController.maxUnits, "Not all spawnpoints have been set");
        Debug.Assert(_player2SpawnPoints.Count == PickMenuController.maxUnits, "Not all spawnpoints have been set");
    }

    public List<Unit> SpawnUnits(List<Cell> cells) {
        _data = FindObjectOfType<DataController>();
        Debug.Assert(_data.playersSelection[0].playerUnits.Length == PickMenuController.maxUnits, "Unit picket by player one are not in correct number");
        Debug.Assert(_data.playersSelection[1].playerUnits.Length == PickMenuController.maxUnits, "Unit picket by player one are not in correct number");

        List<Unit> ret = new List<Unit>();

        for (int i = 0; i < _data.playersSelection[0].playerUnits.Length; i++) {
            GameObject unitInstance = Instantiate(unitClassToPrefab.enumsToPrefab.Find(u => u.unitClass == _data.playersSelection[0].playerUnits[i]).unitPrefab, _player1SpawnPoints[i].position, _player1SpawnPoints[i].rotation) as GameObject;
            Unit unit = PositionateUnitInGrid(cells, unitInstance);
            unit.PlayerNumber = 0;
            ret.Add(unit);
        }

        for (int i = 0; i < _data.playersSelection[0].playerUnits.Length; i++) {
            GameObject unitInstance = Instantiate(unitClassToPrefab.enumsToPrefab.Find(u => u.unitClass == _data.playersSelection[0].playerUnits[i]).unitPrefab, _player2SpawnPoints[i].position, _player2SpawnPoints[i].rotation) as GameObject;
            Unit unit = PositionateUnitInGrid(cells, unitInstance);
            unit.PlayerNumber = 1;
            ret.Add(unit);
        }

        return ret;
    }

    private Unit PositionateUnitInGrid(List<Cell> cells, GameObject unitInstance) {
        unitInstance.transform.SetParent(UnitsParent);
        Unit unit = unitInstance.GetComponent<Unit>();
        if (unit != null) {
            var cell = cells.OrderBy(h => Math.Abs((h.transform.position - unit.transform.position).magnitude)).First();
            if (!cell.IsTaken) {
                cell.IsTaken = true;
                unit.Cell = cell;
                unit.transform.position = cell.transform.position;
                unit.Initialize();
            }
        } else {
            Debug.LogError("Invalid object in Units Parent game object");
        }

        return unit;
    }
}