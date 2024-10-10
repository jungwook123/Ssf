using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Garen_Mk3 : Garen_Mk2
{
    [Header("Garen_Mk3")]
    [SerializeField] Transform swordSpinScaler;
    [SerializeField] protected int swordSpinHitCount;
    [SerializeField] protected int swordSpinDamage;
    [SerializeField] protected float swordSpinDuration;
    [SerializeField] protected Transform swordRotator;
    [SerializeField] protected float swordRotateSpeed;
    [SerializeField] protected EnemyDetector sword1, sword2;
    int swordSpinID;
    protected override void Awake()
    {
        base.Awake();
        sword1.Set(swordSpinDamage, SwordAttack); sword2.Set(swordSpinDamage, SwordAttack);
        swordSpinID = Animator.StringToHash("SwordSpin");
    }
    protected int swordSpinCounter = 0;
    protected float swordSpinTimeCounter = 0.0f;
    bool swordSpinning = false;
    protected override void Attack()
    {
        base.Attack();
        if (!swordSpinning)
        {
            swordSpinCounter++;
            swordSpinScaler.localScale = new Vector2((float)swordSpinCounter / swordSpinHitCount, 1.0f);
            if (swordSpinCounter >= swordSpinHitCount)
            {
                swordSpinCounter = 0;
                anim.SetBool(swordSpinID, true);
                swordSpinTimeCounter = swordSpinDuration;
                swordSpinning = true;
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        if (swordSpinning)
        {
            if (canAttack) swordRotator.eulerAngles = swordRotator.eulerAngles + new Vector3(0, 0, swordRotateSpeed * Time.deltaTime);
            swordSpinTimeCounter = Mathf.Max(0, swordSpinTimeCounter - Time.deltaTime);
            swordSpinScaler.localScale = new Vector2(swordSpinTimeCounter / swordSpinDuration, 1.0f);
            if (swordSpinTimeCounter <= 0)
            {
                anim.SetBool(swordSpinID, false);
                swordSpinning = false;
            }
        }
    }
    void SwordAttack(Enemy enemy)
    {
        if (canAttack)
        {
            GameManager.Instance.UIs.DamageUI(enemy, swordSpinDamage);
            enemy.GetDamage(swordSpinDamage);
        }
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- {swordSpinHitCount}번 공격 후 대검 회전 발동\n- 대검 회전 대미지 {swordSpinDamage}\n- 대검 회전 지속시간 {swordSpinDuration}\n- 대검 회전 속도 {swordRotateSpeed}";
    }
}
