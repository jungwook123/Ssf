using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai : Tower
{
    static AudioClip m_attackClip;
    static AudioClip attackClip { get { if (m_attackClip == null) m_attackClip = Resources.Load<AudioClip>("Audio/Mai_Attack"); return m_attackClip; } }
    public override void Attack()
    {
        base.Attack();
        AudioManager.Instance.PlayAudio(attackClip, 0.5f);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].GetDamage(damage);
    }
}
