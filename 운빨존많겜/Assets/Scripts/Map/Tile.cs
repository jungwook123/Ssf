using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Tile : MonoBehaviour
{
    public int order = 0;
    Tower m_tower;
    public Tower tower { get { return m_tower; } set { m_tower = value; if(m_tower != null) m_tower.sprite.sortingOrder = order; } }
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>(); 
    }
    public void Highlight()
    {
        anim.SetBool("Highlighted", true);
    }
    public void Unhighlight()
    {
        anim.SetBool("Highlighted", false);
    }
}
