using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ignis_Burn : Damage
{
    public Ignis_Burn() : base(Ignis.burnDamage, Ignis.burnTick)
    {

    }
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Ignis_Burn"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
