﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TowerSlotEnterEvent : UnityEvent<int> { }


public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    public bool IsEmpty = true;

    [SerializeField]
    public int Id;
    TowerTypes Type;
    GameObject Tower;

    [SerializeField]
    Color DefaultColor;
    [SerializeField]
    Color HoverColor;

    SpriteRenderer spriteRenderer;
    public TowerSlotEnterEvent towerSlotEnter;
    public TowerSlotEnterEvent towerSlotExit;
    public TowerSlotEnterEvent clicked;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlaceTower(TowerPlacementInfo towerInfo)
    {
        Instantiate(towerInfo.TowerPrefab, transform.position, Quaternion.identity);
        IsEmpty = false;
    }

    private void Update()
    {
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse enter");
        spriteRenderer.color = HoverColor;
        if (towerSlotEnter != null)
            towerSlotEnter.Invoke(Id);
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");
        spriteRenderer.color = DefaultColor;
        if (towerSlotExit != null)
            towerSlotExit.Invoke(Id);
    }

    private void OnMouseUp()
    {
        if (clicked != null)
            clicked.Invoke(Id);
    }

}