using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzTower : Tower
{
    [SerializeField] Transform firePoint;
    [SerializeField] EzBullet bullet;
    [SerializeField] protected float bulletSpeed;
    Pooler<EzBullet> bulletPool;
    protected override void Awake()
    {
        base.Awake();
        bulletPool = new Pooler<EzBullet>(bullet, 100, 0);
    }
    public override void Attack()
    {
        base.Attack();
        bulletPool.GetObject(firePoint.position, Quaternion.Euler(0, 0, Mathf.Atan2(enemies[0].transform.position.y - firePoint.position.y, enemies[0].transform.position.x - firePoint.position.x) * Mathf.Rad2Deg)).Set(damage, bulletSpeed, bulletPool);
    }
}
