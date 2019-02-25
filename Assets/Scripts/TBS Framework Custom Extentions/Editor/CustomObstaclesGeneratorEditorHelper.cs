using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomObstaclesGenerator))]
public class CustomObstaclesGeneratorHelper : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        CustomObstaclesGenerator obstacleGenerator = (CustomObstaclesGenerator)target;

        if (GUILayout.Button("Snap to Grid")) {
            obstacleGenerator.SnapToGrid();
        }
    }
}