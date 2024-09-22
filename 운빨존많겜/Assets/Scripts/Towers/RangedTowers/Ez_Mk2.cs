using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ez_Mk2 : Ez
{
    [Header("Ez_Mk2")]
    [SerializeField] protected float lossHealthAddPercentage;
    [SerializeField] protected float maxBonusDamage;
    protected override void Attack()
    {
        (SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform)) as Ez_Mk2Bullet).Set(lossHealthAddPercentage, maxBonusDamage);
    }
}
