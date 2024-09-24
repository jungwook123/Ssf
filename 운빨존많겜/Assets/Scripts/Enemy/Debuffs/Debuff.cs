using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Debuff
{
    protected readonly DebuffEffect effectOrigin;
    protected float baseDuration;
    public Debuff(float duration, DebuffEffect effectOrigin)
    {
        baseDuration = duration;
        counter = duration;
        this.effectOrigin = effectOrigin;
    }
    protected float counter;
    protected Enemy debuffed;
    protected DebuffEffect effect;
    public virtual void AddDebuff(Enemy debuffed)
    {
        this.debuffed = debuffed;
        debuffed.debuffs.Add(this);
        debuffed.onDeath += RemoveDebuff;
        OnDebuffAdd();
    }
    protected virtual void OnDebuffAdd()
    {
        if (effectOrigin != null) effect = effectOrigin.PlaceEffect(debuffed);
    }
    public virtual void ReApply(Debuff debuff)
    {
        if (debuff.GetType().IsAssignableFrom(GetType()))
        {
            OnDebuffReApply(debuff);
            return;
        }
        else
        {
            RemoveDebuff();
            debuff.AddDebuff(debuffed);
            debuff.OnDebuffReApply(this);
        }
    }
    protected virtual void OnDebuffReApply(Debuff debuff)
    {
        counter = Mathf.Max(counter, debuff.counter);
    }
    public virtual void OnUpdate()
    {
        counter -= Time.deltaTime;
        if(counter <= 0.0f)
        {
            RemoveDebuff();
        }
    }
    bool removed = false;
    public void RemoveDebuff()
    {
        if (removed) return;
        removed = true;
        debuffed.RemoveDebuff(this);
        debuffed.onDeath -= RemoveDebuff;
        OnDebuffEnd();
    }
    protected virtual void OnDebuffEnd()
    {
        if (effect != null) effect.RetractEffect();
    }
}
