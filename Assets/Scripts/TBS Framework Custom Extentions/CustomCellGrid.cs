using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomCellGrid : CellGrid{

    public bool isUnitPlaying = false;

    protected override void Initialize() {
        base.Initialize();
        GetComponent<CustomObstaclesGenerator>()?.SpawnObstacles();
    }

    public void EndTurn(CustomUnit unit) {
        StartCoroutine(WaitForUnitBeforeSkippingTurn(unit));
    }


    private IEnumerator WaitForUnitBeforeSkippingTurn(CustomUnit unit) {
        while (unit.isActing) {
            yield return null;
        }

        yield return null;

        foreach (CustomHumanPlayer player in Players) {
            if (player && player.PlayerUnits.Count == 0)
                yield break;
        }

        EndTurn();
    }
}
