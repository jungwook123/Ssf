using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai : Tower
{
    public override void Attack()
    {
        base.Attack();
        AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Mai_Attack"), 0.5f);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].GetDamage(damage);
    }
}
