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
        if (debuffed.FindDebuff<Ahri_Mk2_Slow>())
        {
            base.DebuffEnd();
            return;
        }
        debuffed.moveSpeed -= Ahri.slowAmount;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    public override void DebuffEnd()
    {
        base.DebuffEnd();
        debuffed.moveSpeed += Ahri.slowAmount;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
