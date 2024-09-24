using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ez_Mk2Bullet : Bullet
{
    [SerializeField] TrailRenderer trail;
    public override void Set(float damage, float speed, Pooler<Bullet> origin)
    {
        base.Set(damage, speed, origin);
        trail.Clear();
    }
    static AudioClip m_hitClip;
    static AudioClip hitClip { get { if (m_hitClip == null) m_hitClip = Resources.Load<AudioClip>("Audio/Ez_Hit"); return m_hitClip; } }
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitClip, 0.5f);
        float dmg = damage + Mathf.Min((hitEnemy.maxHp - hitEnemy.hp) * Ez_Mk2.lossHealthAddPercentage, Ez_Mk2.maxBonusDamage);
        GameManager.Instance.UIs.DamageUI(hitEnemy, dmg);
        hitEnemy.GetDamage(dmg);
    }
}
