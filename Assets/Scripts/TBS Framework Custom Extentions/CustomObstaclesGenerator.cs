using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CustomObstaclesGenerator : MonoBehaviour {

    [SerializeField]
    private Transform _obstaclesParent = null;
    [SerializeField]
    private CellGrid _cellGrid = null;

    private void Start() {
        Debug.Assert(_obstaclesParent != null, "ObstaclesParent is null");
        Debug.Assert(_cellGrid != null, "CellGrid is null");
    }

    public void SpawnObstacles() {

        var cells = _cellGrid.Cells;

        for (int i = 0; i < _obstaclesParent.childCount; i++) {
            var obstacle = _obstaclesParent.GetChild(i);

            var cell = cells.OrderBy(h => Math.Abs((h.transform.position - obstacle.transform.position).magnitude)).First();
            if (!cell.IsTaken) {
                cell.IsTaken = true;
                CustomSquare square = cell as CustomSquare;
                if (square != null) {
                    square.isTakenByObstacle = true;
                }
                var bounds = getBounds(obstacle);
                Vector3 offset = new Vector3(0, bounds.y, 0);
                obstacle.localPosition = cell.transform.localPosition + offset;
            } else {
                Destroy(obstacle.gameObject);
            }
        }
    }

    public void SnapToGrid() {
        List<Transform> cells = new List<Transform>();

        foreach (Transform cell in _cellGrid.transform) {
            cells.Add(cell);
        }

        foreach (Transform obstacle in _obstaclesParent) {
            var bounds = getBounds(obstacle);
            var closestCell = cells.OrderBy(h => Math.Abs((h.transform.position - obstacle.transform.position).magnitude)).First();
            if (!closestCell.GetComponent<Cell>().IsTaken) {
                Vector3 offset = new Vector3(0, bounds.y, 0);
                obstacle.localPosition = closestCell.transform.localPosition + offset;
            }
        }
    }

    private Vector3 getBounds(Transform transform) {
        var renderer = transform.GetComponent<Renderer>();
        var combinedBounds = renderer.bounds;
        var renderers = transform.GetComponentsInChildren<Renderer>();
        foreach (var childRenderer in renderers) {
            if (childRenderer != renderer) combinedBounds.Encapsulate(childRenderer.bounds);
        }

        return combinedBounds.size;
    }
}


