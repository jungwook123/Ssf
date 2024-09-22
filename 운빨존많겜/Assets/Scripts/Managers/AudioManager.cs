using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioManager()
    {
        Instance = this;
    }
    List<AudioSource> sources = new();
    public void PlayAudio(AudioClip clip, float volume)
    {
        foreach(var i in sources)
        {
            if (!i.isPlaying)
            {
                i.clip = clip;
                i.volume = volume;
                i.Play();
                return;
            }
        }
        AudioSource tmp = gameObject.AddComponent<AudioSource>();
        tmp.playOnAwake = false;
        tmp.clip = clip;
        tmp.volume = volume;
        tmp.Play();
        sources.Add(tmp);
    }
    public void PlayAudio(AudioVolumePair pair) => PlayAudio(pair.clip, pair.volume);
    public void PlayAudio(AudioClip clip, float volume, float headStart)
    {
        foreach (var i in sources)
        {
            if (!i.isPlaying)
            {
                i.clip = clip;
                i.volume = volume;
                i.time = headStart;
                i.Play();
                return;
            }
        }
        AudioSource tmp = gameObject.AddComponent<AudioSource>();
        tmp.playOnAwake = false;
        tmp.clip = clip;
        tmp.volume = volume;
        tmp.time = headStart;
        tmp.Play();
        sources.Add(tmp);
    }
    public void PlayAudio(AudioVolumePair pair, float headStart) => PlayAudio(pair.clip, pair.volume, headStart);
}
