using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri : RangedTower
{
    public const float slowAmount = 0.2f, slowTime = 3.0f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ahri_Slow>())
        {
            if (b.FindDebuff<Ahri_Slow>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if(b.FindDebuff<Ahri_Slow>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
}
