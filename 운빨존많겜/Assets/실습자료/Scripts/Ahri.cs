using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Ahri : RangedTower
{
    #region 개발자 전용
    [Header("Ari")]
    [SerializeField] protected float slowScale;
    [SerializeField] protected float slowTime;
    [SerializeField] protected DebuffEffect slowEffect;
    protected override Debuff bulletDebuff => new Ahri_Slow(slowTime, slowEffect, slowScale);
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<Ahri_Slow>())
        {
            if (b.FindDebuff<Ahri_Slow>())
            {
                return base.TargettingCompare(a, b);
            }
            else
            {
                return 1;
            }
        }
        else if(b.FindDebuff<Ahri_Slow>())
        {
            return -1;
        }
        else
        {
            return base.TargettingCompare(a, b);
        }
    }
    protected override void Attack()
    {
        if (GetType() != typeof(Ahri)) base.Attack();
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- 감속 {1.0f - slowScale}";
    }
    #endregion
    //아리가 공격할 때 호출되는 함수
    public void AhriAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        PreAttack();
        #endregion
        //작성...
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(Ahri)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        //작성...
    }
}
