using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Ez_Slow : Slow
{
    public Ez_Slow() : base(Ez.slowAmount)
    {
        
    }
    static Pooler<ParticleSystem> effectPool = new Pooler<ParticleSystem>(Resources.Load<ParticleSystem>("Particles/Ez_Slow"));
    ParticleSystem particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    protected override void OnDebuffEnd()
    {
        base.OnDebuffEnd();
        particles.transform.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
