using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generates rectangular shaped grid of squares.
/// </summary>
[ExecuteInEditMode()]
public class CustomRectangularSquareGridGenerator : RectangularSquareGridGenerator {

    public override List<Cell> GenerateGrid() {
        CellsParent.rotation = Quaternion.identity;

        var ret = base.GenerateGrid();

        CellsParent.rotation = Quaternion.Euler(-90, 0, 0);

        return ret;
    }
}
