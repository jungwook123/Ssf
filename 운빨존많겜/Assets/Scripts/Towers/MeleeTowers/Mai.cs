using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai : Tower
{
    #region 개발자 전용
    [Header("Mai")]
    [SerializeField] protected AudioVolumePair attackSound;
    protected override void Attack()
    {
        AudioManager.Instance.PlayAudio(attackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        MaiAttack(enemies[0]);
    }
    #endregion

    //마스터 이가 공격할 때 호출되는 함수
    public void MaiAttack(Enemy attackedEnemy)
    {
        //범위 안에 있는 적 중에서 제일 앞에 있는 적에게 대미지 주기
        attackedEnemy.GetDamage(damage);
    }
}
