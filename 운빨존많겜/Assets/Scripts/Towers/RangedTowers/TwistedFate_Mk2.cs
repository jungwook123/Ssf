using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistedFate_Mk2 : TwistedFate
{
    protected override Debuff bulletDebuff => new TwistedFate_Mk2_Fate(fateDuration, fateEffect, fateSound, fateDamage);
}
