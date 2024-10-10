using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Modekizer_Mk3 : Modekizer_Mk2
{
    [Header("Modekizer_Mk3")]
    [SerializeField] protected float strikeInjurySlowScale;
    [SerializeField] protected DebuffEffect injuryEffect;
    protected override void Strike()
    {
        enemies[0].AddDebuff(new Modekizer_Mk3_Injury(injuryEffect, strikeInjurySlowScale));
        base.Strike();
    }
    protected override void NormalAttack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].AddDebuff(new Modekizer_Mk3_Bleed(bleedDuration, bleedEffect, bleedSlowScale, bleedDamage));
        enemies[0].GetDamage(damage);
    }
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (count < strikeCount)
        {
            if (a.FindDebuff<Modekizer_Mk3_Bleed>())
            {
                if (b.FindDebuff<Modekizer_Mk3_Bleed>())
                {
                    return base.TargettingCompare(a, b);
                }
                else
                {
                    return 1;
                }
            }
            else if (b.FindDebuff<Modekizer_Mk3_Bleed>())
            {
                return -1;
            }
            else
            {
                return base.TargettingCompare(a, b);
            }
        }
        else
        {
            if (a.FindDebuff<Modekizer_Mk3_Injury>())
            {
                if (b.FindDebuff<Modekizer_Mk3_Injury>())
                {
                    return base.TargettingCompare(a, b);
                }
                else
                {
                    return 1;
                }
            }
            else if (b.FindDebuff<Modekizer_Mk3_Injury>())
            {
                return -1;
            }
            else
            {
                return base.TargettingCompare(a, b);
            }
        }
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- 강타 시 부상 디버프 (감속 {1.0f - strikeInjurySlowScale}, 지속시간 무한)";
    }
}
