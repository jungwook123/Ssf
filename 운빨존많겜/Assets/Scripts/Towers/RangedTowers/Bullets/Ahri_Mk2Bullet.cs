using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk2Bullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Ahri_Hit"), 0.5f);
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage, new Color(0.7f, 0, 0.7f));
        hitEnemy.GetDamage(damage);
        hitEnemy.AddDebuff<Ahri_Mk2_Slow>(Ahri_Mk2.slowTime);
    }
}
