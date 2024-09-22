using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Garen_Mk2_Stun : Debuff
{
    public Garen_Mk2_Stun(float duration, DebuffEffect effectOrigin) : base(duration, effectOrigin)
    {

    }
    public override void AddDebuff(Enemy debuffed)
    {
        if (!debuffed.FindDebuff(this)) base.AddDebuff(debuffed);
    }
    protected override void OnDebuffAdd()
    {
        base.OnDebuffAdd();
        debuffed.canMove--;
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        debuffed.canMove++;
    }
}
