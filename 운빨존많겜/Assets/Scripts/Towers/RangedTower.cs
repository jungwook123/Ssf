using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangedTower : Tower
{
    [Header("Ranged")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected AudioVolumePair hitSound;
    protected virtual Debuff bulletDebuff => null;
    protected override void Attack()
    {
        SpawnBullet(firePoint.position, firePoint.LookAtRot(enemies[0].transform));
    }
    GameObject Instantiate(GameObject prefab, Vector2 position, Quaternion rotation) => prefab.GetComponent<Bullet>().SpawnBullet(position, rotation).gameObject;
    protected Bullet SpawnBullet(Vector2 position, Quaternion rotation)
    {
        Bullet tmp = Instantiate(bullet, position, rotation).GetComponent<Bullet>();
        tmp.Set(damage, bulletSpeed, hitSound, bulletDebuff);
        return tmp;
    }
}
