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
        if(origin.selected.data.upgrade != null)
        {
            origin.UIs.fuseButton.gameObject.SetActive(true);
            CheckFuseAvailability(origin.selected.data);
            GameManager.Instance.onTowerSpawn += CheckFuseAvailability;
        }
        else
        {
            origin.UIs.fuseButton.gameObject.SetActive(false);
        }
        origin.UIs.SelectUI(origin.selected);
        origin.rangeViewer.position = origin.selected.transform.position;
        origin.rangeViewer.localScale = new Vector2(origin.selected.range * 2.0f, origin.selected.range * 2.0f);
        origin.selected.Select();
        origin.rangeViewer.gameObject.SetActive(true);
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            parentLayer.ChangeState("Idle"); return;
        }
        if (Input.GetMouseButtonDown(0) && origin.UIs._IsMouseOnUI()==false || Input.GetKeyDown(KeyCode.Escape))
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
        GameManager.Instance.UpgradeTower(origin.selected.data);
        parentLayer.ChangeState("Idle");
    }
    void CheckFuseAvailability(TowerData data)
    {
        if(data == origin.selected.data)
        {
            if(GameManager.Instance.SearchTower(data) >= 3)
            {
                origin.UIs.fuseButton.interactable = true;
            }
            else
            {
                origin.UIs.fuseButton.interactable = false;
            }
        }
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        origin.UIs.CloseSelectUI();
        if (origin.selected.data.upgrade != null)
        {
            GameManager.Instance.onTowerSpawn -= CheckFuseAvailability;
        }
        origin.selected.Unselect();
        origin.selected = null;
        origin.rangeViewer.gameObject.SetActive(false);
    }
}