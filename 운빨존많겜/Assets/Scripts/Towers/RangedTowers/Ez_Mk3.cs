using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ez_Mk3 : Ez_Mk2
{
    [Header("Ez_Mk3")]
    [SerializeField] Transform tripleShotScaler;
    [SerializeField] protected int tripleShotHitCount = 8;
    [SerializeField] protected float tripleShotSpread = 5.0f;
    protected int count = 0;
    protected override void Attack()
    {
        if(count < tripleShotHitCount)
        {
            count++;
            tripleShotScaler.localScale = new Vector2((float)count / tripleShotHitCount, 1.0f);
            base.Attack();
        }
        else
        {
            count = 0;
            tripleShotScaler.localScale = new Vector2((float)count / tripleShotHitCount, 1.0f);
            SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform) * Quaternion.Euler(0, 0, -tripleShotSpread));
            SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform));
            SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform) * Quaternion.Euler(0, 0, tripleShotSpread));
        }
    }
}
