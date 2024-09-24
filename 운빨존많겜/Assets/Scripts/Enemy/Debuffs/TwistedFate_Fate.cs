using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TwistedFate_Fate : Debuff
{
    public int count { get; private set; }
    public TwistedFate_Fate()
    {
        count = 1;
    }
    static Pooler<Transform> effectPool = new Pooler<Transform>(Resources.Load<Transform>("Particles/TwistedFate_Fate"));
    Transform particles;
    public override void Set(float duration, Enemy debuffed)
    {
        base.Set(duration, debuffed);
        particles = effectPool.GetObject(debuffed.transform.position, Quaternion.identity, debuffed.transform);
        particles.GetChild(1).gameObject.SetActive(false);
        particles.GetChild(2).gameObject.SetActive(false);
    }
    public override void ResetDuration(float duration)
    {
        if (ended) return;
        base.ResetDuration(duration);
        count++;
        if(count >= 4)
        {
            DebuffEnd();
            AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/TwistedFate_Fate"), 0.8f);
            GameManager.Instance.UIs.DamageUI(debuffed, TwistedFate.fateDamage);
            debuffed.GetDamage(TwistedFate.fateDamage);
        }
        else
        {
            particles.transform.GetChild(count-1).gameObject.SetActive(true);
        }
    }
    bool ended = false;
    public override void DebuffEnd()
    {
        if (ended) return;
        base.DebuffEnd();
        particles.SetParent(null);
        effectPool.ReleaseObject(particles);
        ended = true;
    }
}
