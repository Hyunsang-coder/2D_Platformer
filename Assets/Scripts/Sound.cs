using System;
using UnityEngine;


[Serializable]
public class Sound
{
    public string name;
    public AudioClip _clip;

    public bool _loop;

    [Range(0f, 1f)]
    public float _volume = 1;


    [HideInInspector]
    public AudioSource audioSource;
}
