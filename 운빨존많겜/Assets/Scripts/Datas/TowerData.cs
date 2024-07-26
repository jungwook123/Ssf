using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptables/Tower Data", order = 0)]
public class TowerData : ScriptableObject
{
    [SerializeField] Tower m_tower;
    [SerializeField] Sprite m_cardImage;
    [SerializeField] TowerData m_upgrade;
    public Tower tower { get { return m_tower; } }
    public Sprite cardImage { get { return m_cardImage; } }
    public TowerData upgrade { get {  return m_upgrade; } }
}
