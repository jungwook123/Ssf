using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeTower : Tower
{
    public override void Attack()
    {
        enemies[0].GetDamage(damage * towerCount);
        anim.SetTrigger("Attack");
    }
}
