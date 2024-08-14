using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modekizer_Mk2 : Tower
{
    public const float bleedDamage = 10f, bleedTick = 0.5f, bleedDuration = 3f, bleedSlowScale = 0.8f;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if(count < strikeCount)
        {
            if (a.FindDebuff<Modekizer_Mk2_HeavyBleed>())
            {
                if (b.FindDebuff<Modekizer_Mk2_HeavyBleed>())
                {
                    return base.TargettingCompare(a, b);
                }
                else
                {
                    return 1;
                }
            }
            else if (b.FindDebuff<Modekizer_Mk2_HeavyBleed>())
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
    int count = 0;
    const int strikeCount = 4;
    const float strikeDamage = 50.0f;
    public override void Attack()
    {
        base.Attack();
        if(count < strikeCount)
        {
            AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Modekizer_Attack"), 0.5f);
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
            enemies[0].AddDebuff<Modekizer_Mk2_HeavyBleed>(bleedDuration);
            enemies[0].GetDamage(damage);
            count++;
        }
        else
        {
            AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Modekizer_Strike"), 0.8f);
            GameManager.Instance.UIs.DamageUI(enemies[0], strikeDamage);
            enemies[0].GetDamage(strikeDamage);
            count = 0;
        }
    }
}
