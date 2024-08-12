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
    protected override void OnHit(Enemy hitEnemy)
    {
        float dmg = damage + Mathf.Min((hitEnemy.maxHp - hitEnemy.hp) * Ez_Mk2.lossHealthAddPercentage, Ez_Mk2.maxBonusDamage);
        GameManager.Instance.UIs.DamageUI(hitEnemy, dmg);
        hitEnemy.GetDamage(dmg);
    }
}
