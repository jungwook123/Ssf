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
        debuffed.moveSpeed *= Ahri.slowScale;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    bool ended = false;
    public override void DebuffEnd()
    {
        if (ended) return;
        base.DebuffEnd();
        debuffed.moveSpeed /= Ahri.slowScale;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
        ended = true;
    }
}
