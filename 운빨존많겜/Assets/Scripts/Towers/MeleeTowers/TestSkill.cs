using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestSkill : MeleeTower
{
    [SerializeField] float skillChance = 10.0f;
    public override void Attack()
    {
        base.Attack();
        if(Random.Range(0.0f, 100.0f) <= skillChance) anim.Play("SwordSpin", 1);
    }
}
