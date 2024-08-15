using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzBullet : Bullet
{
    static AudioClip m_hitClip;
    static AudioClip hitClip { get { if (m_hitClip == null) m_hitClip = Resources.Load<AudioClip>("Audio/Ez_Hit"); return m_hitClip; } }
    protected override void OnHit(Enemy hitEnemy)
    {
        base.OnHit(hitEnemy);
        AudioManager.Instance.PlayAudio(hitClip, 0.5f);
    }
}
