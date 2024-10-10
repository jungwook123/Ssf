using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modekizer_Mk2 : Modekizer
{
    [Header("Modekizer_Mk2")]
    [SerializeField] protected int strikeCount = 4;
    [SerializeField] protected float strikeDamage = 50.0f;
    [SerializeField] AudioVolumePair strikeSound;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if(count < strikeCount)
        {
            if (a.FindDebuff<Modekizer_Mk2_Bleed>())
            {
                if (b.FindDebuff<Modekizer_Mk2_Bleed>())
                {
                    return base.TargettingCompare(a, b);
                }
                else
                {
                    return 1;
                }
            }
            else if (b.FindDebuff<Modekizer_Mk2_Bleed>())
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
            return base.TargettingCompare(a, b);
        }
    }
    protected int count = 0;
    protected override void Attack()
    {
        if(count < strikeCount)
        {
            NormalAttack();
            count++;
        }
        else
        {
            Strike();
            count = 0;
        }
    }
    protected virtual void Strike()
    {
        AudioManager.Instance.PlayAudio(strikeSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], strikeDamage);
        enemies[0].GetDamage(strikeDamage);
    }
    protected virtual void NormalAttack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].AddDebuff(new Modekizer_Mk2_Bleed(bleedDuration, bleedEffect, bleedSlowScale, bleedDamage));
        enemies[0].GetDamage(damage);
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- {strikeCount+1}번째 공격은 강타 (데미지 {strikeDamage})";
    }
}
