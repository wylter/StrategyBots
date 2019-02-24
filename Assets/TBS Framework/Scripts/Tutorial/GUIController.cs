using System;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public CellGrid CellGrid;
	
    void Awake(){ 
        CellGrid.LevelLoading += onLevelLoading;
        CellGrid.LevelLoadingDone += onLevelLoadingDone;
    }

    private void onLevelLoading(object sender, EventArgs e){
        Debug.Log("Level is loading");
    }

    private void onLevelLoadingDone(object sender, EventArgs e){
        Debug.Log("Level loading done");
    }

    public void NotifyEndTurn() {
        CellGrid.EndTurn();
    }
}
