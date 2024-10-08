using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk2 : Ahri
{
    protected override Debuff bulletDebuff => new Ahri_Mk2_Slow(slowTime, slowEffect, slowScale);
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
