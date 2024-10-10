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
    public override string Describe()
    {
        return base.Describe() + $"\n- 적이 읽은 체력의 {lossHealthAddPercentage * 100}% 추가 대미지 (최대 {maxBonusDamage})";
    }
}
