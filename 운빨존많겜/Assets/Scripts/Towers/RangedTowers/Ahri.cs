using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri : RangedTower
{
    [Header("Ari")]
    [SerializeField] protected float slowScale;
    [SerializeField] protected float slowTime;
    [SerializeField] protected DebuffEffect slowEffect;
    protected override Debuff bulletDebuff => new Ahri_Slow(slowTime, slowEffect, slowScale);
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
