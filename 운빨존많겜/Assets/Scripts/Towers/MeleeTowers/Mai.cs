using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai : Tower
{
    [Header("Mai")]
    [SerializeField] protected AudioVolumePair attackSound;
    protected override void Attack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].GetDamage(damage);
    }
}
