using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mai_Mk2 : Mai
{
    [Header("Mai_Mk2")]
    [SerializeField] GameObject buffAura;
    [SerializeField] Transform buffScaler;
    [SerializeField] protected int buffHitCount = 10;
    [SerializeField] protected float buffDuration = 5.0f, buffScale = 1.5f;
    [SerializeField] protected AudioVolumePair enhancedAttackSound;
    int count = 0;
    bool buffed = false;
    [NonSerialized] float counter = 0.0f;
    protected override void Attack()
    {
        if (buffed)
        {
            BuffAttack();
        }
        else
        {
            AudioManager.Instance.PlayAudio(attackSound);
            GameManager.Instance.UIs.DamageUI(enemies[0], damage);
            enemies[0].GetDamage(damage);
            count++;
            buffScaler.localScale = new Vector2((float)count / buffHitCount, 1.0f);
            if (count >= buffHitCount)
            {
                count = 0;
                buffAura.SetActive(true);
                counter = buffDuration;
                buffed = true;
            }
        }
    }
    protected virtual void BuffAttack()
    {
        AudioManager.Instance.PlayAudio(enhancedAttackSound);
        GameManager.Instance.UIs.DamageUI(enemies[0], damage * buffScale);
        enemies[0].GetDamage(damage * buffScale);
    }
    protected override void Update()
    {
        base.Update();
        if (buffed)
        {
            counter = Mathf.Max(0.0f, counter - Time.deltaTime);
            buffScaler.localScale = new Vector2(counter / buffDuration, 1.0f);
            if (counter <= 0)
            {
                buffAura.SetActive(false);
                buffed = false;
            }
        }
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- {buffHitCount}번 공격 후 버프 발동\n- 버프 중 대미지 배율 {buffScale}\n- 버프 지속시간 {buffDuration}";
    }
}
