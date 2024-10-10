using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk3 : Ahri_Mk2
{
    [Header("Ahri_Mk3")]
    [SerializeField] float spread = 15.0f;
    protected override Debuff bulletDebuff => new Ahri_Mk3_Slow(slowTime, slowEffect, slowScale);
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ahri_Mk3_Slow>())
        {
            if (b.FindDebuff<Ahri_Mk3_Slow>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if(b.FindDebuff<Ahri_Mk3_Slow>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
    protected override void Attack()
    {
        SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform) * Quaternion.Euler(0, 0, -spread));
        SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform));
        SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform) * Quaternion.Euler(0, 0, spread));
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- 한번에 탄환 3개 발사";
    }
}
