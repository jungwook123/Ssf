using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Damage : Debuff
{
    readonly float damage, tickRate;
    public Damage(float damage, float tickRate)
    {
        this.damage = damage;
        this.tickRate = tickRate;
    }

    float counter = 0.0f;
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (counter < tickRate) counter += Time.deltaTime;
        else
        {
            counter -= tickRate;
            debuffed.GetDamage(damage);
        }
    }
}
