using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFateBullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        base.OnHit(hitEnemy);
        hitEnemy.AddDebuff<TwistedFate_Fate>(TwistedFate.fateDuration);
    }
}
