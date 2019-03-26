using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "TBSDataObjects/UnitClassEnumToPrefab", order = 2)]
public class UnitClassEnumToPrefab : ScriptableObject {
    [System.Serializable]
    public class EnumAndPrefab {
        public UnitClass unitClass;
        public GameObject unitPrefab;
    }

    public List<EnumAndPrefab> enumsToPrefab;
}
