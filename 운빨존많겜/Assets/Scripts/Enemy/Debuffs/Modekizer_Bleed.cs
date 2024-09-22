using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Modekizer_Bleed : Debuff
{
    protected readonly float slowScale, bleedDamage, bleedTick;
    public Modekizer_Bleed(float duration, DebuffEffect effectOrigin, float slowScale, float bleedDamage, float bleedTick = 0.5f) : base(duration, effectOrigin)
    {
        this.slowScale = slowScale;
        this.bleedDamage = bleedDamage;
        this.bleedTick = bleedTick;
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
    float damageCounter = 0;
    public override void OnUpdate()
    {
        base.OnUpdate();
        damageCounter += Time.deltaTime;
        if(damageCounter >= bleedTick)
        {
            damageCounter -= bleedTick;
            BleedDamage();
        }
    }
    protected virtual void BleedDamage()
    {
        GameManager.Instance.UIs.DamageUI(debuffed, bleedDamage, new Color(0.7f, 0, 0));
        debuffed.GetDamage(bleedDamage);
    }
}
public class Modekizer_Mk2_Bleed : Modekizer_Bleed
{
    public Modekizer_Mk2_Bleed(float duration, DebuffEffect effectOrigin, float slowScale, float bleedDamage, float bleedTick = 0.5F) : base(duration, effectOrigin, slowScale, bleedDamage, bleedTick)
    {

    }
    protected override void BleedDamage()
    {
        GameManager.Instance.UIs.DamageUI(debuffed, bleedDamage, new Color(0.5f, 0, 0));
        debuffed.GetDamage(bleedDamage);
    }
}
public class Modekizer_Mk3_Bleed : Modekizer_Mk2_Bleed
{
    public Modekizer_Mk3_Bleed(float duration, DebuffEffect effectOrigin, float slowScale, float bleedDamage, float bleedTick = 0.5F) : base(duration, effectOrigin, slowScale, bleedDamage, bleedTick)
    {

    }
    protected override void BleedDamage()
    {
        GameManager.Instance.UIs.DamageUI(debuffed, bleedDamage, new Color(0.35f, 0, 0));
        debuffed.GetDamage(bleedDamage);
    }
}
