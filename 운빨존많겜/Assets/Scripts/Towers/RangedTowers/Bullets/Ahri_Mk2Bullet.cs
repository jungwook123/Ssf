using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahri_Mk2Bullet : Bullet
{
    protected override void OnHit(Enemy hitEnemy)
    {
        AudioManager.Instance.PlayAudio(hitSound);
        GameManager.Instance.UIs.DamageUI(hitEnemy, damage, new Color(0.7f, 0, 0.7f));
        if (inflictingDebuff != null) hitEnemy.AddDebuff(inflictingDebuff);
        hitEnemy.GetDamage(damage);
    }
}
