using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mai_Mk2 : Tower
{
    [Header("Mai_Mk2")]
    [SerializeField] GameObject buffAura;
    [SerializeField] Transform buffScaler;
    const int buffHitCount = 10;
    const float buffDuration = 5.0f, buffScale = 1.5f;
    int count = 0;
    bool buffed = false;
    float counter = 0.0f;
    public override void Attack()
    {
        base.Attack();
        if (buffed)
        {
            GameManager.Instance.UIs.DamageUI(enemies[0], damage * buffScale);
            enemies[0].GetDamage(damage * buffScale);
        }
        else
        {
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
    protected override void Update()
    {
        base.Update();
        if (buffed)
        {
            if (counter > 0.0f)
            {
                counter = Mathf.Max(0.0f, counter - Time.deltaTime);
                buffScaler.localScale = new Vector2(counter / buffDuration, 1.0f);
            }
            else
            {
                buffAura.SetActive(false);
                buffed = false;
            }
        }
    }
}
