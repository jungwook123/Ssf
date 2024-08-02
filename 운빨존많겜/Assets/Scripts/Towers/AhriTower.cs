using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriTower : Tower
{
    [SerializeField] Transform firePoint;
    [SerializeField] AhriBullet bullet;
    [SerializeField] protected float bulletSpeed;
    Pooler<AhriBullet> bulletPool;
    protected override void Awake()
    {
        base.Awake();
        bulletPool = new Pooler<AhriBullet>(bullet, 100, 0);
    }
    public override void Attack()
    {
        base.Attack();
        bulletPool.GetObject(firePoint.position, Quaternion.Euler(0, 0, Mathf.Atan2(enemies[0].transform.position.y - firePoint.position.y, enemies[0].transform.position.x - firePoint.position.x) * Mathf.Rad2Deg)).Set(damage, bulletSpeed, bulletPool);
    }
}
