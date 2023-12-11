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


public class CubeSoundManager : SingletonHandler<CubeSoundManager>
{


    private AudioSource[] _audioSources;


    public override void Awake()
    {
        base.Awake();

        _audioSources = new AudioSource[(int)AudioType.Length - 1];


        for(int i = 0, count = (int)AudioType.Length; i < count; i++)
        {
            GameObject source = new GameObject(Enum.GetName(typeof(AudioType), i));
            source.AddComponent<AudioSource>();
            source.transform.parent = transform;
            _audioSources[i] = source.GetComponent<AudioSource>();
        }

        _audioSources[(int)AudioType.Effect].loop = false;
        _audioSources[(int)AudioType.Effect].playOnAwake = false;


    }


}
