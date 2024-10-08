using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate : RangedTower
{
    [Header("TwistedFate")]
    [SerializeField] protected float fateDamage;
    [SerializeField] protected float fateDuration;
    [SerializeField] protected AudioVolumePair fateSound;
    [SerializeField] protected DebuffEffect fateEffect;
    protected override Debuff bulletDebuff => new TwistedFate_Fate(fateDuration, fateEffect, fateSound, fateDamage);
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<TwistedFate_Fate>())
        {
            if (b.FindDebuff<TwistedFate_Fate>())
            {
                return b.GetDebuff<TwistedFate_Fate>().count.CompareTo(a.GetDebuff<TwistedFate_Fate>().count);
            }
            else return -1;
        }
        else if (b.FindDebuff<TwistedFate_Fate>())
        {
            return 1;
        }
        else return base.TargettingCompare(a, b);
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- 적에게 '페이트' 디버프 부여 (4번 중첩 시 페이트 데미지 발생)\n- 페이트 대미지 {fateDamage}\n- 페이트 지속시간 {fateDuration}";
    }
}
