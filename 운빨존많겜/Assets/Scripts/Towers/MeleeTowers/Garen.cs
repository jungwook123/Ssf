using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Garen : Tower
{
    [Header("Garen")]
    [SerializeField] protected float threshold = 0.33f;
    [SerializeField] protected float multiplier = 3f;
    [SerializeField] protected AudioVolumePair attackSound;
    protected override void Attack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        ThresholdCheckAttack();
    }
    protected void ThresholdCheckAttack()
    {
        if (enemies[0].hp / enemies[0].maxHp <= threshold)
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage * multiplier);
            enemies[0].GetDamage(damage * multiplier);
        }
        else
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
            enemies[0].GetDamage(damage);
        }
    }
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.hp / a.maxHp <= threshold)
        {
            if (b.hp / b.maxHp <= threshold)
            {
                return base.TargettingCompare(a, b);
            }
            else return -1;
        }
        else if (b.hp / b.maxHp <= threshold)
        {
            return 1;
        }
        else return base.TargettingCompare(a, b);
    }
}
