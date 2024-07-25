using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptables/Tower Data", order = 0)]
public class TowerData : ScriptableObject
{
    [SerializeField] GameObject m_tower;
    public GameObject tower { get { return m_tower; } }
}
