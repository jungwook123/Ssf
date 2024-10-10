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
    Tile aimedTile = null;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        aimedTile = null;
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.0f, LayerMask.GetMask("Tile"));
        if (hit)
        {
            if(aimedTile == null || hit.transform != aimedTile.transform)
            {
                aimedTile = hit.transform.GetComponent<Tile>();
                if(aimedTile.tower != null)
                {
                    origin.UIs.towerDescUI.gameObject.SetActive(true);
                    origin.UIs.SetDesc(aimedTile.tower);
                }
            }
            if(aimedTile.tower != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    origin.selected = aimedTile.tower;
                    origin.selectedTile = aimedTile;
                    parentLayer.ChangeState("MouseHeld");
                    return;
                }
                else
                {
                    origin.UIs.towerDescUI.position = Input.mousePosition;
                }
            }
        }
        else
        {
            aimedTile = null;
            origin.UIs.towerDescUI.gameObject.SetActive(false);
        }
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        origin.UIs.towerDescUI.gameObject.SetActive(false);
    }
}