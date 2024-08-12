using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai : Tower
{
    public override void Attack()
    {
        base.Attack();
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        enemies[0].GetDamage(damage);
    }
}
