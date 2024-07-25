using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Tile : MonoBehaviour
{
    public Tower tower;
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
