using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TwistedFate_Fate : Debuff
{
    public int count { get; private set; }
    protected readonly float fateDamage;
    protected readonly AudioVolumePair fateSound;
    public TwistedFate_Fate(float duration, DebuffEffect effectOrigin, AudioVolumePair fateSound, float fateDamage = 30, int startStack = 1) : base(duration, effectOrigin)
    {
        count = startStack;
        this.fateDamage = fateDamage;
        this.fateSound = fateSound;
    }
    public override void AddDebuff(Enemy debuffed)
    {
        if(!debuffed.FindDebuff(this)) base.AddDebuff(debuffed);
    }
    protected override void OnDebuffReApply(Debuff debuff)
    {
        base.OnDebuffReApply(debuff);
        count += (debuff as TwistedFate_Fate).count;
        if (count >= 4)
        {
            Fate();
            RemoveDebuff();
        }
        else
        {
            for (int i = 1; i < count; i++)
            {
                effect.EnableLevel(i);
            }
        }
    }
    protected virtual void Fate()
    {
        AudioManager.Instance.PlayAudio(fateSound);
        GameManager.Instance.UIs.DamageUI(debuffed, fateDamage);
        debuffed.GetDamage(fateDamage);
    }
}
public class TwistedFate_Mk2_Fate : TwistedFate_Fate
{
    public TwistedFate_Mk2_Fate(float duration, DebuffEffect effectOrigin, AudioVolumePair fateSound, float fateDamage = 30, int startStack = 1) : base(duration, effectOrigin, fateSound, fateDamage, startStack)
    {
        
    }
}
public class TwistedFate_Mk3_Fate : TwistedFate_Mk2_Fate
{
    readonly Bullet fateCard;
    readonly float bulletDamage, bulletSpeed;
    readonly AudioVolumePair bulletHitSound;
    readonly int shotCount;
    readonly int maxPulseHit;
    public TwistedFate_Mk3_Fate(float duration, DebuffEffect effectOrigin, AudioVolumePair fateSound, Bullet fateCard, float bulletDamage, float bulletSpeed, AudioVolumePair bulletHitSound, int shotCount, int maxPulseHit, float fateDamage = 30, int startStack = 1) : base(duration, effectOrigin, fateSound, fateDamage, startStack)
    {
        this.fateCard = fateCard;
        this.bulletDamage = bulletDamage;
        this.bulletSpeed = bulletSpeed;
        this.bulletHitSound = bulletHitSound;
        this.shotCount = shotCount;
        this.maxPulseHit = maxPulseHit;
    }
    protected override void Fate()
    {
        base.Fate();
        HitCountException hE = new HitCountException(maxPulseHit);
        for (int i = 0; i < shotCount; i++)
        {
            Bullet tmp = fateCard.SpawnBullet(debuffed.transform.position, Quaternion.Euler(0, 0, 360.0f / shotCount * i));
            tmp.Set(bulletDamage, bulletSpeed, bulletHitSound, new TwistedFate_Mk3_Fate(baseDuration, effectOrigin, fateSound, fateCard, bulletDamage, bulletSpeed, bulletHitSound, shotCount, maxPulseHit, fateDamage, 1));
            tmp.SetException(hE, new ExcludeSpecificException(debuffed));
        }
    }
}
