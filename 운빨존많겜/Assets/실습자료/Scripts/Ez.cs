using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ez : RangedTower
{
    #region 개발자 전용
    protected override void Attack()
    {
        if (GetType() != typeof(Ez)) base.Attack();
    }
    #endregion
    //에즈리얼이 공격할 때 호출되는 함수
    public void EzAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        PreAttack();
        #endregion
        //작성..
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(Ez)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        //작성...
    }
}
