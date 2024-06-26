using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangedTower : Tower
{
    [SerializeField] Transform[] firePoints = new Transform[3];
    [SerializeField] GameObject bullet;
    [SerializeField] protected float bulletSpeed;
    Pooler bulletPool;
    protected override void Awake()
    {
        base.Awake();
        bulletPool = new Pooler(bullet, 100, 0);
    }
    public override void Attack()
    {
        foreach(var i in firePoints)
        {
            Bullet bul = bulletPool.GetObject(i.position, Quaternion.Euler(0, 0, Mathf.Atan2(enemies[0].transform.position.y - i.position.y, enemies[0].transform.position.x - i.position.x) * Mathf.Rad2Deg)).GetComponent<Bullet>();
            bul.Set(damage, bulletSpeed, bulletPool);
        }
        anim.SetTrigger("Attack");
    }
}
