using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ahri_Slow : Debuff
{
    readonly float slowScale;
    public Ahri_Slow(float duration, DebuffEffect effectOrigin, float slowScale) : base(duration, effectOrigin)
    {
        this.slowScale = slowScale;
    }
    public override void AddDebuff(Enemy debuffed)
    {
        if (!debuffed.FindDebuff(this)) base.AddDebuff(debuffed);
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
public class Ahri_Mk2_Slow : Ahri_Slow
{
    public Ahri_Mk2_Slow(float duration, DebuffEffect effectOrigin, float slowScale) : base(duration, effectOrigin, slowScale)
    {

    }
}

public class Ahri_Mk3_Slow : Ahri_Mk2_Slow
{
    public Ahri_Mk3_Slow(float duration, DebuffEffect effectOrigin, float slowScale) : base(duration, effectOrigin, slowScale)
    {

    }
}