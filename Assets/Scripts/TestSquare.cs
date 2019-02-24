using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSquare : Square{

    [SerializeField]
    private Color m_highlightedColor;
    [SerializeField]
    private Color m_pathColor;
    [SerializeField]
    private Color m_reachableColor;

    private SpriteRenderer renderer;
    private Color m_defaultColor;
    

    
    public void Start() {
        renderer = GetComponent<SpriteRenderer>();
        m_defaultColor = renderer.color;
    }
    

    public override Vector3 GetCellDimensions() {
        return GetComponent<SpriteRenderer>().bounds.size;
    }
    public override void MarkAsHighlighted() {
        renderer.color = m_highlightedColor;
    }
    public override void MarkAsPath() {
        renderer.color = m_pathColor;
    }
    public override void MarkAsReachable() {
        renderer.color = m_reachableColor;
    }
    public override void UnMark() {
        renderer.color = m_defaultColor;
    }
}
