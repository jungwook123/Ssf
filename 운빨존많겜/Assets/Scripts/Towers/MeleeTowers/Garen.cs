using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Garen : Tower
{
    const float threshold = 0.33f, multiplier = 3f;
    public override void Attack()
    {
        base.Attack();
        if(enemies[0].hp / enemies[0].maxHp <= threshold)
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage * multiplier, new Color32(255, 225, 112, 255));
            enemies[0].GetDamage(damage * multiplier);
        }
        else
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
            enemies[0].GetDamage(damage);
        }
    }
}
