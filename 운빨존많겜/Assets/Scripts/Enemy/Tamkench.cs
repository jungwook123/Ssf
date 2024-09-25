using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tamkench : Boss
{
    #region 개발자 전용
    protected override void Awake()
    {
        base.Awake();
        PlayAppearSound();
    }
    void PlaySound(AudioVolumePair sound) => AudioManager.Instance.PlayAudio(sound);
    #endregion
    public AudioVolumePair tamkenchSound;
    //유니티에서 등장 소리 넣어주기 위한 변수 선언

    //탐켄치가 등장할 때 호출되는 함수
    void PlayAppearSound()
    {
        tamkenchSound.Play();
        //탐켄치 등장 소리 플레이해주기
    }
}