using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Modekizer_Mk3_Injury : Debuff
{
    readonly float slowScale;
    public Modekizer_Mk3_Injury(DebuffEffect effectOrigin, float slowScale) : base(99999, effectOrigin)
    {
        this.slowScale = slowScale;
    }
    public override void AddDebuff(Enemy debuffed)
    {
        if(!debuffed.FindDebuff(this)) base.AddDebuff(debuffed);
    }
    protected override void OnDebuffAdd()
    {
        base.OnDebuffAdd();
        debuffed.moveSpeed *= slowScale;
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        debuffed.moveSpeed /= slowScale;
    }
}