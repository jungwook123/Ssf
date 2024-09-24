using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Stun : Debuff
{
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Stun"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        debuffed.DisableMovement();
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    public override void DebuffEnd()
    {
        base.DebuffEnd();
        debuffed.EnableMovement();
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
