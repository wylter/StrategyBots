using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generates rectangular shaped grid of squares.
/// </summary>
[ExecuteInEditMode()]
public class CustomRectangularSquareGridGenerator : RectangularSquareGridGenerator {

    /*
    public override List<Cell> GenerateGrid() {
        CellsParent.rotation = Quaternion.identity;

        var ret = base.GenerateGrid();

        CellsParent.rotation = Quaternion.Euler(-90, 0, 0);

        return ret;
    }
    */

    public override List<Cell> GenerateGrid() {
        CellsParent.rotation = Quaternion.identity;

        var ret = new List<Cell>();

        if (SquarePrefab.GetComponent<Square>() == null) {
            Debug.LogError("Invalid square cell prefab provided");
            return ret;
        }

        for (int i = -Width/2; i < Width/2; i++) {
            for (int j = -Height/2; j < Height/2; j++) {
                var square = Instantiate(SquarePrefab);
                var squareSize = square.GetComponent<Cell>().GetCellDimensions();

                square.transform.position = new Vector3(i * squareSize.x + squareSize.x/2, 0, j * squareSize.z + squareSize.z / 2);
                square.GetComponent<Cell>().OffsetCoord = new Vector2(i, j);
                square.GetComponent<Cell>().MovementCost = 1;
                ret.Add(square.GetComponent<Cell>());

                square.transform.parent = CellsParent;
            }
        }

        CellsParent.rotation = Quaternion.Euler(-90, 0, 0);

        return ret;
    }
}
