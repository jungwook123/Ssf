using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignis : MeleeTower
{
    public const float burnDamage = 3f, burnTick = 0.5f, burnDuration = 5.0f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ignis_Burn>())
        {
            if (b.FindDebuff<Ignis_Burn>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if (b.FindDebuff<Ignis_Burn>())
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
        enemies[0].AddDebuff<Ignis_Burn>(burnDuration);
    }
}
