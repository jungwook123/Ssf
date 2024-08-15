using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk2Bullet : Bullet
{
    static AudioClip m_hitClip;
    static AudioClip hitClip { get { if (m_hitClip == null) m_hitClip = Resources.Load<AudioClip>("Audio/Ahri_Hit"); return m_hitClip; } }
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitClip, 0.5f);
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage, new Color(0.7f, 0, 0.7f));
        hitEnemy.GetDamage(damage);
        hitEnemy.AddDebuff<Ahri_Mk2_Slow>(Ahri_Mk2.slowTime);
    }
}
