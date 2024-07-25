using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeTower : Tower
{
    public override void Attack()
    {
        base.Attack();
        enemies[0].GetDamage(damage * towerCount);
    }
}
