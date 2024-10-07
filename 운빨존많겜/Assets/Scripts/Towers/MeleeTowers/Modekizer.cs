using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modekizer : Tower
{
    [Header("Modekizer")]
    [SerializeField] protected float bleedDamage = 7f;
    [SerializeField] protected float bleedTick = 0.5f, bleedDuration = 2f, bleedSlowScale = 0.9f;
    [SerializeField] protected AudioVolumePair attackSound;
    [SerializeField] protected DebuffEffect bleedEffect;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Modekizer_Bleed>())
        {
            if (b.FindDebuff<Modekizer_Bleed>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if (b.FindDebuff<Modekizer_Bleed>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
    protected override void Attack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].AddDebuff(new Modekizer_Bleed(bleedDuration, bleedEffect, bleedSlowScale, bleedDamage));
        enemies[0].GetDamage(damage);
    }
}
