using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager_Grabbing : State<GameManager>
{
    public GameManager_Grabbing(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        origin.selected.Disable();
        tile = origin.selectedTile;
        tile.Highlight();
    }
    Tile tile = null; 
    public override void OnStateUpdate()
    {
        origin.selected.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        origin.selected.transform.position = new Vector3(origin.selected.transform.position.x, origin.selected.transform.position.y, 0.0f);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.0f, LayerMask.GetMask("Tile"));
        if(hit && hit.transform != tile?.transform)
        {
            tile.Unhighlight();
            tile = hit.transform.GetComponent<Tile>();
            tile.Highlight();
        }
        if (Input.GetMouseButton(0) == false)
        {
            origin.selected.transform.position = tile.transform.position;
            if(tile != origin.selectedTile)
            {
                origin.selectedTile.tower = null;
                tile.tower = origin.selected;
                origin.selectedTile = tile;
            }
            parentLayer.ChangeState("Selected");
        }
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        if (tile != null)
        {
            tile.Unhighlight();
            tile = null;
        }
        origin.selected.Enable();
    }
}