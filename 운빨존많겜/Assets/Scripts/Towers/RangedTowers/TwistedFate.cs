using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate : RangedTower
{
    public const float fateDamage = 30.0f, fateDuration = 5.0f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<TwistedFate_Fate>())
        {
            if (b.FindDebuff<TwistedFate_Fate>())
            {
                return b.GetDebuff<TwistedFate_Fate>().count.CompareTo(a.GetDebuff<TwistedFate_Fate>().count);
            }
            else return -1;
        }
        else if (b.FindDebuff<TwistedFate_Fate>())
        {
            return 1;
        }
        else return base.TargettingCompare(a, b);
    }
}
