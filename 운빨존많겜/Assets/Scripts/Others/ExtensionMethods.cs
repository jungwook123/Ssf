using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
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
    public static bool IsSameClass(this object obj1, object obj2)
    {
        return obj1.GetType().IsAssignableFrom(obj2.GetType()) && obj2.GetType().IsAssignableFrom(obj1.GetType());
    }
    public static void Play(this AudioVolumePair sound) => AudioManager.Instance.PlayAudio(sound);
}
[System.Serializable]
public struct AudioVolumePair
{
    [SerializeField] AudioClip m_clip;
    [Range(0, 1)][SerializeField] float m_volume;
    public AudioClip clip { get { return m_clip; } }
    public float volume { get { return m_volume; } }
}
public struct Stackbool
{
    public static implicit operator bool(Stackbool sb)
    {
        return sb.count == 0;
    }
    public static Stackbool operator --(Stackbool sb)
    {
        sb.count++;
        return sb;
    }
    public static Stackbool operator ++(Stackbool sb)
    {
        sb.count--;
        return sb;
    }
    int count;
}