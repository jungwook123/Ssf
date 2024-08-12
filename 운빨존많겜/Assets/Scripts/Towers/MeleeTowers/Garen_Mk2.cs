using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Garen_Mk2 : Tower
{
    const float threshold = 0.5f, multiplier = 3f;
    const int stunCount = 4;
    const float stunDuration = 1.0f;
    int count = 0;
    public override void Attack()
    {
        base.Attack();
        if (count < stunCount) count++;
        else
        {
            count = 0;
            enemies[0].AddDebuff<Stun>(stunDuration);
        }
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
