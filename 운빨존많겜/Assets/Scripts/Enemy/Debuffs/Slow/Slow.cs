using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Slow : Debuff
{
    readonly float slowAmount;
    public Slow(float slowAmount)
    {
        this.slowAmount = slowAmount;
    }
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        debuffed.moveSpeed -= slowAmount;
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        debuffed.moveSpeed += slowAmount;
    }
}
