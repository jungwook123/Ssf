using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ez_Mk2Bullet : Bullet
{
    float lossHealthAddPercentage, maxBonusDamage;
    public void Set(float lossHealthAddPercentage, float maxBonusDamage)
    {
        this.lossHealthAddPercentage = lossHealthAddPercentage;
        this.maxBonusDamage = maxBonusDamage;
    }
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitSound);
        float dmg = damage + Mathf.Min((hitEnemy.maxHp - hitEnemy.hp) * lossHealthAddPercentage, maxBonusDamage);
        GameManager.Instance.UIs.DamageUI(hitEnemy, dmg);
        hitEnemy.GetDamage(dmg);
    }
}
