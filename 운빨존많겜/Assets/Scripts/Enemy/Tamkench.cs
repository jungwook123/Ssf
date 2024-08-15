using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tamkench : Boss
{
    protected override void Awake()
    {
        base.Awake();
        AudioManager.Instance.PlayAudio(Resources.Load<AudioClip>("Audio/Tamkench"), 1.0f);
    }
}