using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ahri_Slow : Debuff
{
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Ahri_Slow"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        debuffed.moveSpeed -= Ahri.slowAmount;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        debuffed.moveSpeed += Ahri.slowAmount;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
