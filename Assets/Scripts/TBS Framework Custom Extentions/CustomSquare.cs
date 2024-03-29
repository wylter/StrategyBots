﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomSquare : Square{

    [SerializeField]
    private Color _highlightedColor = Color.white; //Color to set when highlighting the square
    [SerializeField]
    private Color _pathColor = Color.white; //Color to set when highlighting the square as path
    [SerializeField]
    private Color _reachableColor = Color.white; //Color to set when highlighting the square as reachable
    [SerializeField]
    private Color _attackableColor = Color.white; //Color to set when highlighting the square as attackable
    [SerializeField]
    private Color _reachableByAbilityColor = Color.white; //Color to set when highlighting the square as attackable

    private SpriteRenderer _spriteRenderer; //Sprite rendere component of the prefab
    private Color _defaultColor; //Default color set for the prefab

    private CustomUnit _unit = null;
    public CustomUnit unit { get { return _unit; } set { _unit = value; } }

    [HideInInspector]
    public bool isTakenByObstacle;
    

    
    public void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;

        Debug.Assert(_spriteRenderer != null, "SpriteRenderer not found");
    }
    
    //This is access in Edit mode to generate the grid
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

    public void MarkAsAttackable() {
        _spriteRenderer.color = _attackableColor;
    }

    public void MarkAsReachableByAbility() {
        _spriteRenderer.color = _reachableByAbilityColor;
    }


    public override void UnMark() {
        _spriteRenderer.color = _defaultColor;
    }
}
