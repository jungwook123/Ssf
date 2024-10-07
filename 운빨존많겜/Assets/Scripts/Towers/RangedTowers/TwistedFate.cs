using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate : RangedTower
{
    #region 개발자 전용
    [Header("TwistedFate")]
    [SerializeField] protected float fateDamage;
    [SerializeField] protected float fateDuration;
    [SerializeField] protected AudioVolumePair fateSound;
    [SerializeField] protected DebuffEffect fateEffect;
    protected override Debuff bulletDebuff => new TwistedFate_Fate(fateDuration, fateEffect, fateSound, fateDamage);
    protected override int TargettingCompare(Enemy a, Enemy b)
    {
        if (a.FindDebuff<TwistedFate_Fate>())
        {
            if (b.FindDebuff<TwistedFate_Fate>())
            {
                return b.GetDebuff<TwistedFate_Fate>().count.CompareTo(a.GetDebuff<TwistedFate_Fate>().count);
            }
            else return -1;
        }
        else if (b.FindDebuff<TwistedFate_Fate>())
        {
            return 1;
        }
        else return base.TargettingCompare(a, b);
    }
    protected override void Attack()
    {
        if (GetType() != typeof(TwistedFate)) base.Attack();
    }
    #endregion
    //트위스티드 페이트가 공격할 때 호출되는 함수
    public void TwistedFateAttack(Enemy attackedEnemy)
    {
        #region 개발자 전용
        PreAttack();
        #endregion
        Vector2 bulletPosition = firePoint.position;
        //생성할 총알의 위치값을 발사 지점의 위치로 설정

        Quaternion bulletRotation = firePoint.LookAtRot(attackedEnemy.transform);
        //생성할 총알의 회전값을 발사 지점을 기준으로 제일 앞 적을 바라보는 회전값으로 설정

        Bullet spawnedBullet = SpawnBullet(bulletPosition, bulletRotation);
        //아까 정의한 위치값과 회전값으로 총알 생성

        spawnedBullet.damage = damage;
        spawnedBullet.speed = bulletSpeed;
        //생성한 총알의 대미지랑 속도 지정
    }
    float timer = 0.0f;
    protected override void Update()
    {
        #region 개발자 전용
        base.Update();
        if (GetType() != typeof(TwistedFate)) return;
        enemies.RemoveAll((Enemy i) => i == null);
        enemies.Sort((Enemy a, Enemy b) => TargettingCompare(a, b));
        #endregion
        timer += Time.deltaTime;
        if (canAttack && timer >= fireRate && enemies.Count > 0)
        {
            timer = 0.0f;
            TwistedFateAttack(enemies[0]);
        }
    }
}
