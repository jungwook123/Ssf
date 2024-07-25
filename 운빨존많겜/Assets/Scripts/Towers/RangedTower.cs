using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangedTower : Tower
{
    [SerializeField] Transform[] firePoints = new Transform[3];
    [SerializeField] Bullet bullet;
    [SerializeField] protected float bulletSpeed;
    Pooler<Bullet> bulletPool;
    protected override void Awake()
    {
        base.Awake();
        bulletPool = new Pooler<Bullet>(bullet, 100, 0);
    }
    public override void Attack()
    {
        base.Attack();
        for(int i = 0; i < towerCount; i++)
        {
            bulletPool.GetObject(firePoints[i].position, Quaternion.Euler(0, 0, Mathf.Atan2(enemies[0].transform.position.y - firePoints[i].position.y, enemies[0].transform.position.x - firePoints[i].position.x) * Mathf.Rad2Deg)).Set(damage, bulletSpeed, bulletPool);
        }
    }
}
