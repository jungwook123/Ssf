using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager_MouseHeld : State<GameManager>
{
    public GameManager_MouseHeld(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if (Input.GetMouseButtonUp(0))
        {
            parentLayer.ChangeState("Selected");
        }
        else if(Input.GetAxis("Mouse X") >= 0.1f || Input.GetAxis("Mouse X") <= -0.1f || Input.GetAxis("Mouse Y") >= 0.1f || Input.GetAxis("Mouse Y") <= -0.1f)
        {
            parentLayer.ChangeState("Grabbing");
        }
    }
}