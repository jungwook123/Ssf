using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Garen_Mk2 : Garen
{
    [Header("Garen_Mk2")]
    [SerializeField] protected int stunCount = 4;
    [SerializeField] protected float stunDuration = 1.0f;
    [SerializeField] protected AudioVolumePair stunSound;
    [SerializeField] protected DebuffEffect stunEffect;
    int count = 0;
    protected override void Attack()
    {
        if (count < stunCount)
        {
            AudioManager.Instance.PlayAudio(attackSound);
            count++;
        }
        else
        {
            AudioManager.Instance.PlayAudio(stunSound);
            count = 0;
            enemies[0].AddDebuff(new Garen_Mk2_Stun(stunDuration, stunEffect));
        }
        ThresholdCheckAttack();
    }
    public override string Describe()
    {
        return base.Describe() + $"\n- {stunCount+1}번째 공격 {stunDuration}초 스턴";
    }
}
