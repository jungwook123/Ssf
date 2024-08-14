using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzBullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        base.OnHit(hitEnemy);
        AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Ez_Hit"), 0.5f);
    }
}
