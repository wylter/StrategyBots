using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomCellGrid : CellGrid{

    public override void StartGame() {
        Debug.Assert(Players.Count == 2, "Player count is not 2");

        OnGameStarted(new EventArgs());

        Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u => { u.OnTurnStart(); });

        List<Unit> units = Units.FindAll(u => u.PlayerNumber.Equals(0));
        //Players[0].

        Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)).Play(this);
    }
}
