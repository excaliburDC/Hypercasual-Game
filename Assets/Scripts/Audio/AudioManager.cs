using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonManager<AudioManager>
{
    private static readonly string bgmString = "BGM";
    private static readonly string sfxString = "SFX";
    public List<Sounds> sounds;

    [HideInInspector] public float bgmFloat, sfxFloat;


    [SerializeField] private float delay = 1f;




    private void OnEnable()
    {
        bgmFloat = PlayerPrefs.GetFloat(bgmString);
        sfxFloat = PlayerPrefs.GetFloat(sfxString);
        Debug.Log(bgmFloat + " " + sfxFloat);
        
        SetupAudio();
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("MenuSound");
    }

    private void SetupAudio()
    {

        foreach (Sounds s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            if (s.audioType.ToString() == bgmString)
                s.audioSource.volume = bgmFloat;

            else if (s.audioType.ToString() == sfxString)
                s.audioSource.volume = sfxFloat;

            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found..!!!");
            return;

        }
        s.audioSource.Play();
    }

    public void FadePause(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);

        if (s.audioSource.isPlaying)
        {
            StartCoroutine(FadeSound(s));

        }
    }

    public void Pause(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);

        if (s.audioSource.isPlaying)
        {
            s.audioSource.Pause();

        }
    }

    public void Stop(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);

        if (s.audioSource.isPlaying)
        {
            s.audioSource.Stop();

        }
    }

    public void Resume(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);

        if (!s.audioSource.isPlaying)
        {
            s.audioSource.UnPause();

        }
    }

    public void SetBGMVolume(float volume)
    {
        //mixerGroup.audioMixer.SetFloat("Music", volume);

        foreach (Sounds s in sounds)
        {
            if (s.audioType.ToString() == bgmString)
            {
                s.audioSource.volume = volume;
                bgmFloat = volume;
                PlayerPrefs.SetFloat(bgmString, bgmFloat);
                Debug.Log(PlayerPrefs.GetFloat(bgmString));


            }

        }

    }

    public void SetSFXVolume(float volume)
    {
        //mixerGroup.audioMixer.SetFloat("Music", volume);
        foreach (Sounds s in sounds)
        {
            if (s.audioType.ToString() == sfxString)
            {
                s.audioSource.volume = volume;
                sfxFloat = volume;
                PlayerPrefs.SetFloat(sfxString, sfxFloat);
                Debug.Log(PlayerPrefs.GetFloat(sfxString));
            }
        }

    }

    IEnumerator FadeSound(Sounds currentSound)
    {
        float elapsedTime = 0;
        float currentVolume = currentSound.audioSource.volume;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            currentSound.audioSource.volume = Mathf.Lerp(currentVolume, 0f, elapsedTime / delay);

            if (currentSound.audioSource.volume <= 0f)
                currentSound.audioSource.Pause();


            yield return null;

        }
    }
}
