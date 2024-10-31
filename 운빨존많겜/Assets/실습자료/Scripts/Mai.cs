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

    }
    #endregion

    //마스터 이가 공격할 때 호출되는 함수
    public void MaiAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        AudioManager.Instance.PlayAudio(attackSound);
        PreAttack();
        GameManager.Instance.UIs.DamageUI(enemies[0], damage);
        #endregion
        //작성...
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(Mai)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        timer += Time.deltaTime;
        if (canAttack && timer >= fireRate && enemies.Count > 0)
        {
            timer = 0.0f;
            MaiAttack(enemies[0]);
        }
    }
}
