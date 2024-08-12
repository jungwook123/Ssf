using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modekizer : Tower
{
    public const float bleedDamage = 7f, bleedTick = 0.5f, bleedDuration = 2f, bleedSlow = 0.1f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Modekizer_Bleed>())
        {
            if (b.FindDebuff<Modekizer_Bleed>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if (b.FindDebuff<Modekizer_Bleed>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
    public override void Attack()
    {
        base.Attack();
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].AddDebuff<Modekizer_Bleed>(bleedDuration);
        enemies[0].GetDamage(damage);
    }
}
