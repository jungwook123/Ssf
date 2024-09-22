using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestSkill : Tower
{
    [SerializeField] float skillChance = 10.0f;
    [SerializeField] EnemyDetector sword1, sword2;
    protected override void Attack()
    {
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        if (Random.Range(0.0f, 100.0f) <= skillChance) anim.Play("SwordSpin", 1);
        enemies[0].GetDamage(damage);
    }
    public override void Disable()
    {
        base.Disable();
        anim.speed = 0.0f;
        sword1.enabled = false;
        sword2.enabled = false;
    }
    public override void Enable()
    {
        base.Enable();
        anim.speed = 1.0f;
        sword1.enabled = true;
        sword2.enabled = true;
    }
}
