using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Modekizer_Bleed : Debuff
{
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Modekizer_Bleed"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        if (debuffed.FindDebuff<Modekizer_Mk2_HeavyBleed>())
        {
            base.DebuffEnd();
            return;
        }
        debuffed.moveSpeed *= Modekizer.bleedSlowScale;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    float counter = 0;
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (counter < Modekizer.bleedTick) counter += Time.deltaTime;
        else
        {
            counter -= Modekizer.bleedTick;
            GameManager.Instance.UIs.DamageUI(debuffed, Modekizer.bleedDamage, new Color(0.7f, 0, 0));
            debuffed.GetDamage(Modekizer.bleedDamage);
        }
    }
    public override void DebuffEnd()
    {
        base.DebuffEnd();
        debuffed.moveSpeed /= Modekizer.bleedSlowScale;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
    }
}
