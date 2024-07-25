using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_TopLayer : TopLayer<GameManager>
{
    public GameManager_TopLayer(GameManager origin) : base(origin)
    {
        defaultState = new GameManager_Idle(origin, this);
        AddState("Idle", defaultState);
        AddState("MouseHeld", new GameManager_MouseHeld(origin, this));
        AddState("Grabbing", new GameManager_Grabbing(origin, this));
        AddState("Selected", new GameManager_Selected(origin, this));
        origin.onGameOver += () => { ChangeState("Idle"); };
    }
    public override void ChangeState(string stateName)
    {
        currentState.OnStateExit();
        currentState = states[stateName];
        currentState.OnStateEnter();
    }
}