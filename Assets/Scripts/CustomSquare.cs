﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomSquare : Square{

    [SerializeField]
    private Color _highlightedColor; //Color to set when highlighting the square
    [SerializeField]
    private Color _pathColor; //Color to set when highlighting the square as path
    [SerializeField]
    private Color _reachableColor; //Color to set when highlighting the square as reachable

    private SpriteRenderer _spriteRenderer; //Sprite rendere component of the prefab
    private Color _defaultColor; //Default color set for the prefab
    

    
    public void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }
    

    public override Vector3 GetCellDimensions() {
        return GetComponent<SpriteRenderer>().bounds.size;
    }

    public override void MarkAsHighlighted() {
        _spriteRenderer.color = _highlightedColor;
    }

    public override void MarkAsPath() {
        _spriteRenderer.color = _pathColor;
    }

    public override void MarkAsReachable() {
        _spriteRenderer.color = _reachableColor;
    }

    public override void UnMark() {
        _spriteRenderer.color = _defaultColor;
    }
}