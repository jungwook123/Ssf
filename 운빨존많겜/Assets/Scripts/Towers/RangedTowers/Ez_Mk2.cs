using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ez_Mk2 : Ez
{
    [Header("Ez_Mk2")]
    [SerializeField] protected float lossHealthAddPercentage;
    [SerializeField] protected float maxBonusDamage;
    protected override Bullet SpawnBullet(Vector2 position, Quaternion rotation)
    {
        Bullet tmp = base.SpawnBullet(position, rotation);
        (tmp as Ez_Mk2Bullet).Set(lossHealthAddPercentage, maxBonusDamage);
        return tmp;
    }
}
