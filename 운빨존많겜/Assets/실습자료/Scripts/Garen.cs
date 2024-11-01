using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Garen : Tower
{
    [Header("Garen")]
    public float threshold = 0.33f;
    public float multiplier = 3f;
    #region 개발자 전용
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
    public override string Describe()
    {
        return base.Describe() + $"\n- 대상의 체력이 {threshold * 100}% 이하일 때 대미지 {multiplier}배";
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
        //작성..
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
