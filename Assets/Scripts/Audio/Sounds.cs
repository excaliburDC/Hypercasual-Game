using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    BGM,
    SFX
}

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;

    public AudioType audioType;

    public bool loop;

    [HideInInspector] public AudioSource audioSource;
}
