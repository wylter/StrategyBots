using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "TBSDataObjects/PlayerSelection", order = 1)]
public class PlayerSelection : ScriptableObject {
    public UnitClass[] playerUnits = null;
}
