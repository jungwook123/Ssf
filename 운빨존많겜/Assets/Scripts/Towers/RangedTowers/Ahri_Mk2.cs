using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk2 : RangedTower
{
    public const float slowAmount = 0.6f, slowTime = 7.0f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ahri_Mk2_Slow>())
        {
            if (b.FindDebuff<Ahri_Mk2_Slow>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if(b.FindDebuff<Ahri_Mk2_Slow>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
}
