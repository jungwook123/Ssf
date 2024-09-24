using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[DisallowMultipleComponent]
public class DebuffEffect : MonoBehaviour
{
    static Dictionary<DebuffEffect, Pooler<DebuffEffect>> effectPools = new();
    [SerializeField] GameObject[] levels;
    bool isInstantiated = false;
    Pooler<DebuffEffect> origin;
    public void OnEnable()
    {
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }
    public void EnableLevel(int level)
    {
        if (!isInstantiated) return;
        if (levels.Length > level) levels[level].SetActive(true);
    }
    public DebuffEffect PlaceEffect(Enemy enemy)
    {
        if (isInstantiated) return null;
        if (!effectPools.ContainsKey(this))
        {
            effectPools.Add(this, new Pooler<DebuffEffect>(this));
        }
        DebuffEffect tmp = effectPools[this].GetObject(enemy.transform.position, Quaternion.identity, enemy.transform);
        tmp.isInstantiated = true;
        tmp.origin = effectPools[this];
        return tmp;
    }
    public void RetractEffect()
    {
        if (!isInstantiated) return;
        transform.SetParent(null);
        origin.ReleaseObject(this);
    }
}