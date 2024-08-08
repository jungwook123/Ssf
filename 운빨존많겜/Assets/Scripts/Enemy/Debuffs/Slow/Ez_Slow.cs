using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ez_Slow : Slow
{
    public Ez_Slow() : base(Ez.slowAmount)
    {
        
    }
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Ez_Slow"));
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
