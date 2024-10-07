using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Garen : Tower
{
    #region 개발자 전용
    [Header("Garen")]
    [SerializeField] protected float threshold = 0.33f;
    [SerializeField] protected float multiplier = 3f;
    [SerializeField] protected AudioVolumePair attackSound;
    protected override void Attack()
    {

    }
    protected void ThresholdCheckAttack()
    {
        if (enemies[0].hp / enemies[0].maxHp <= threshold)
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage * multiplier);
            enemies[0].GetDamage(damage * multiplier);
        }
        else
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
            enemies[0].GetDamage(damage);
        }
    }
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.hp / a.maxHp <= threshold)
        {
            if (b.hp / b.maxHp <= threshold)
            {
                return base.TargettingCompare(a, b);
            }
            else return -1;
        }
        else if (b.hp / b.maxHp <= threshold)
        {
            return 1;
        }
        else return base.TargettingCompare(a, b);
    }
    #endregion
    //가렌이 공격할 때 호출되는 함수
    public void GarenAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        AudioManager.Instance.PlayAudio(attackSound);
        if (enemies[0].hp / enemies[0].maxHp <= threshold)
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage * multiplier);
        }
        else
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        }
        PreAttack();
        #endregion
        //적 체력의 비율 (적의 현재 체력 / 적의 최대 체력) 확인
        if (attackedEnemy.hp / attackedEnemy.maxHp <= threshold)
        {
            attackedEnemy.GetDamage(damage * multiplier);
            //적 체력의 비율이 설정된 양 이하면 설정된 양만큼 곱해서 대미지 주기
        }
        else
        {
            attackedEnemy.GetDamage(damage);
            //적 체력이 비율이 설정된 양보다 많으면 그냥 대미지 주기
        }
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(Garen)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        timer += Time.deltaTime;
        if (canAttack && timer >= fireRate && enemies.Count > 0)
        {
            timer = 0.0f;
            GarenAttack(enemies[0]);
        }
    }
}
