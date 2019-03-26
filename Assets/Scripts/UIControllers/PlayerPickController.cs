using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerPickController : MonoBehaviour{

    [SerializeField]
    private int _playerNumber = 0;

    private PickMenuController _pickController;
    private bool[] _occupiedSpots;

    private void Start() {
        _pickController = FindObjectOfType<PickMenuController>();
        _occupiedSpots = new bool[PickMenuController.maxUnits];
    }

    public void NotifyPlayerInput(int pick) {
        UnitOverlayController unitOverlay = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<UnitOverlayController>();
        Debug.Assert(unitOverlay != null, "Unit Overlay Controller not found");

        int spot = unitOverlay.currentSpot;

        if (spot < 0) {
            spot = findNextSpot(0);
            if (spot < 0) {
                return;
            }
            unitOverlay.UpdateOverlay(spot);
            _pickController.NotifyPick(_playerNumber, (UnitClass)pick, spot);
        } else {
            _occupiedSpots[spot] = false;
            spot = findNextSpot(spot+1);
            unitOverlay.UpdateOverlay(spot);
            if (spot > 0) {
                _pickController.NotifyChangeSpotPick(_playerNumber, (UnitClass)pick, spot);
            } else {
                _pickController.NotifyDeletePick(_playerNumber);
            }
        }

    }

    public int findNextSpot(int start) {
        while (start < PickMenuController.maxUnits) {
            if (!_occupiedSpots[start]) {
                _occupiedSpots[start] = true;
                return start;
            }
            start++;
        }
        return -1;
    }
}
