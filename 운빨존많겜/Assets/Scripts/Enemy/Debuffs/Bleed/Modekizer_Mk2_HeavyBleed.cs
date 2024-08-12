using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Modekizer_Mk2_HeavyBleed : Debuff
{
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/Modekizer_Mk2_HeavyBleed"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);

        Modekizer_Bleed tmp = debuffed.GetDebuff<Modekizer_Bleed>();
        if (tmp != null) tmp.DebuffEnd();

        debuffed.moveSpeed *= Modekizer_Mk2.bleedSlowScale;
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
    }
    float counter = 0;
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (counter < Modekizer_Mk2.bleedTick) counter += Time.deltaTime;
        else
        {
            counter -= Modekizer_Mk2.bleedTick;
            GameManager.Instance.UIs.DamageUI(debuffed, Modekizer_Mk2.bleedDamage, new Color(0.5f, 0, 0));
            debuffed.GetDamage(Modekizer_Mk2.bleedDamage);
        }
    }
    bool ended = false;
    public override void DebuffEnd()
    {
        if (ended) return;
        base.DebuffEnd();
        debuffed.moveSpeed /= Modekizer_Mk2.bleedSlowScale;
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
        ended = true;
    }
}
