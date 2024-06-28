using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethods
{
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    public static Quaternion LookAtRot(this Transform from, Transform lookAt)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(lookAt.position.y - from.position.y, lookAt.position.x - from.position.x) * Mathf.Rad2Deg);
    }
}