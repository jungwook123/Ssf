using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ahri_Mk2_Slow : Debuff
{
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Ahri_Mk2_Slow"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);

        Ahri_Slow tmp = debuffed.GetDebuff<Ahri_Slow>();
        if (tmp != null) tmp.DebuffEnd();

        debuffed.moveSpeed *= Ahri_Mk2.slowScale;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    bool ended = false;
    public override void DebuffEnd()
    {
        if (ended) return;
        base.DebuffEnd();
        debuffed.moveSpeed /= Ahri_Mk2.slowScale;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
        ended = true;
    }
}
