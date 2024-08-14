using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFateBullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        base.OnHit(hitEnemy);
        AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/TwistedFate_Hit"), 0.5f);
        hitEnemy.AddDebuff<TwistedFate_Fate>(TwistedFate.fateDuration);
    }
}
