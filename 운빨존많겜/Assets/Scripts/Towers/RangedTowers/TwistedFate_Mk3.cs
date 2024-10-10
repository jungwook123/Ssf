using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate_Mk3 : TwistedFate_Mk2
{
    [Header("TwistedFate_Mk3")]
    [SerializeField] protected float pulseBulletDamage;
    [SerializeField] protected float pulseBulletSpeed;
    [SerializeField] protected int pulseBulletCount;
    [SerializeField] protected int maxPulseHit;
    protected override Debuff bulletDebuff => new TwistedFate_Mk3_Fate(fateDuration, fateEffect, fateSound, bullet.GetComponent<Bullet>(), pulseBulletDamage, pulseBulletSpeed, hitSound, pulseBulletCount, maxPulseHit, null, fateDamage);
    public override string Describe()
    {
        return base.Describe() + $"\n- 페이트 대미지 발생 시 해당 적 주위로 {pulseBulletCount}개의 카드 생성\n- 생성된 카드 대미지 {pulseBulletDamage}\n- 생성된 카드 속도 {pulseBulletSpeed}\n- 1회 폭발 시 최대 카드 피격 개수 {maxPulseHit}";
    }
}
