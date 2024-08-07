using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ez : RangedTower
{
    public const float slowAmount = 0.2f, slowTime = 3.0f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ez_Slow>())
        {
            if (b.FindDebuff<Ez_Slow>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if (b.FindDebuff<Ez_Slow>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
}
