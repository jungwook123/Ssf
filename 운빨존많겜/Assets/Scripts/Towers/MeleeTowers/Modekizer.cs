using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modekizer : Tower
{
    #region 개발자 전용
    [Header("Modekizer")]
    [SerializeField] protected float bleedDamage = 7f;
    [SerializeField] protected float bleedTick = 0.5f, bleedDuration = 2f, bleedSlowScale = 0.9f;
    [SerializeField] protected AudioVolumePair attackSound;
    [SerializeField] protected DebuffEffect bleedEffect;
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Modekizer_Bleed>())
        {
            if (b.FindDebuff<Modekizer_Bleed>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if (b.FindDebuff<Modekizer_Bleed>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
    void InflictBleedingEffect(Enemy enemy)
    {
        enemy.AddDebuff(new Modekizer_Bleed(bleedDuration, bleedEffect, bleedSlowScale, bleedDamage));
    }
    protected override void Attack()
    {

    }
    #endregion
    //모데카이저가 공격할 때 호출되는 함수
    public void ModekizerAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        AudioManager.Instance.PlayAudio(attackSound);
        PreAttack();
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        #endregion
        attackedEnemy.GetDamage(damage);
        //적에게 대미지룰 주고...

        InflictBleedingEffect(attackedEnemy);
        //그 적에게 출혈 효과를 준다.
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(Modekizer)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        timer += Time.deltaTime;
        if (canAttack && timer >= fireRate && enemies.Count > 0)
        {
            timer = 0.0f;
            ModekizerAttack(enemies[0]);
        }
    }
}
