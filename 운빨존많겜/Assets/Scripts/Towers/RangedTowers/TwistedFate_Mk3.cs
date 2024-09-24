using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate_Mk3 : TwistedFate_Mk2
{
    [Header("TwistedFate_Mk3")]
    [SerializeField] protected float pulseBulletDamage;
    [SerializeField] protected float pulseBulletSpeed;
    [SerializeField] protected int pulseBulletCount;
    [SerializeField] protected int maxPulseHit;
    protected override Debuff bulletDebuff => new TwistedFate_Mk3_Fate(fateDuration, fateEffect, fateSound, bullet, pulseBulletDamage, pulseBulletSpeed, hitSound, pulseBulletCount, maxPulseHit, null, fateDamage);
}
