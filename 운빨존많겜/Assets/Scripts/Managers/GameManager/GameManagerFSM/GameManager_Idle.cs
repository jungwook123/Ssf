using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager_Idle : State<GameManager>
{
    public GameManager_Idle(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.0f, LayerMask.GetMask("Tile"));
            if (hit)
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                if (tile.tower != null)
                {
                    origin.selected = tile.tower;
                    origin.selectedTile = tile;
                    parentLayer.ChangeState("MouseHeld");
                    return;
                }
            }
        }
    }
}