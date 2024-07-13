using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager_Selected : State<GameManager>
{
    public GameManager_Selected(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
    bool added = false;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        if (!added)
        {
            origin.UIs.deleteButton.onClick.AddListener(DeleteTower);
            origin.UIs.fuseButton.onClick.AddListener(FuseTower);
            added = true;
        }
        if (origin.selected == null)
        {
            parentLayer.ChangeState("Idle"); return;
        }
        origin.UIs.SelectUI(origin.selected);
        origin.selected.Select();
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            parentLayer.ChangeState("Idle"); return;
        }
        if (Input.GetMouseButtonDown(0) && origin.UIs._IsMouseOnUI()==false)
        {
            parentLayer.ChangeState("Idle"); return;
        }
        /*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 0.0f, LayerMask.GetMask("Tile"));
        if (hit)
        {
            if (hit.transform != tile?.transform)
            {
                tile = hit.transform.GetComponent<Tile>();
            }
            if (tile.tower != null) return;
            origin.moveArrow.position = origin.selected.transform.position;
            origin.moveArrow.rotation = origin.selected.transform.LookAtRot(hit.transform);
            origin.moveArrow.localScale = new Vector2(Vector2.Distance(origin.selected.transform.position, hit.transform.position), 1.0f);
            origin.moveArrow.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                hit.transform.GetComponent<Tile>().tower = origin.selected;
                origin.selected.Move(hit.transform);
                origin.selectedTile.tower = null;
                parentLayer.ChangeState("Idle"); return;
            }
        }
        else
        {
            origin.moveArrow.gameObject.SetActive(false);
        }*/
    }
    void DeleteTower()
    {
        MonoBehaviour.Destroy(origin.selected.gameObject);
        parentLayer.ChangeState("Idle");
    }
    void FuseTower()
    {
        parentLayer.ChangeState("Idle");
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        origin.UIs.CloseSelectUI();
        origin.selected.Unselect();
        origin.selected = null;
    }
}