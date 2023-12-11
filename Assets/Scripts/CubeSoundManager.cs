using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    Background,
    Effect,
    Length
}


public class CubeSoundManager : MonoBehaviour
{
    public static CubeSoundManager Instance;

    private AudioSource[] _audioSources;


    public void Awake()
    {


        if (CubeSoundManager.Instance == null)
        {
            CubeSoundManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        _audioSources = new AudioSource[(int)AudioType.Length];

        for (int i = 0, count = (int)AudioType.Length; i < count; i++)
        {
            GameObject source = new GameObject(Enum.GetName(typeof(AudioType), i));
            source.transform.parent = transform;
            _audioSources[i] = source.AddComponent<AudioSource>();
        }

        _audioSources[(int)AudioType.Effect].loop = false;
        _audioSources[(int)AudioType.Effect].playOnAwake = false;

        _audioSources[(int)AudioType.Background].loop = true;
        _audioSources[(int)AudioType.Background].playOnAwake = true;
        _audioSources[(int)AudioType.Background].volume = 0.1f;
    }


    public void PlayAudio(AudioClip clip, AudioType type, float volume = 1, float pitch = 1)
    {
        int typeTotInt = (int)type;

        _audioSources[typeTotInt].pitch = pitch;

        _audioSources[typeTotInt].clip = clip;
        _audioSources[typeTotInt].Play();
    }

}
