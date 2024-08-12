using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Debuff
{
    float counter;
    protected Enemy debuffed;
    public virtual void Set(float duration, Enemy debuffed)
    {
        this.debuffed = debuffed;
        counter = duration;
        debuffed.onDeath += DebuffEnd;
    }
    public virtual void ResetDuration(float duration)
    {
        counter = Mathf.Max(counter, duration);
    }
    public virtual void OnUpdate()
    {
        counter -= Time.deltaTime;
        if(counter <= 0.0f)
        {
            DebuffEnd();
        }
    }
    public virtual void DebuffEnd()
    {
        debuffed.RemoveDebuff(this);
        debuffed.onDeath -= DebuffEnd;
    }
}
