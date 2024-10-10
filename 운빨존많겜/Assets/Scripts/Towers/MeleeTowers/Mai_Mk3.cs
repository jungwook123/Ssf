using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mai_Mk3 : Mai_Mk2
{
    [Header("Mai_Mk3")]
    [SerializeField] protected int maxBuffHitCount;
    protected override void BuffAttack()
    {
        for (int i = 1; i < Mathf.Min(enemies.Count, maxBuffHitCount); i++)
        {
            GameManager.Instance.UIs.DamageUI(enemies[i], damage * buffScale);
            enemies[i].GetDamage(damage * buffScale);
        }
        base.BuffAttack();
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- 버프 도중 {maxBuffHitCount}마리의 적 동시 공격 가능";
    }
}