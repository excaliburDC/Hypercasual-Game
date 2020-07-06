using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonManager<AudioManager>
{
    public List<Sounds> sounds;

    [SerializeField] private float delay = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        foreach (Sounds s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("MenuSound");
    }

    public void Play(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound: " + name + " not found..!!!");
            return;

        }
        s.audioSource.Play();
    }

    public void FadePause(string name)
    {
        Sounds s = sounds.Find(sound => sound.name == name);

        if(s.audioSource.isPlaying)
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

    IEnumerator FadeSound(Sounds currentSound)
    {
        float elapsedTime = 0;
        float currentVolume = currentSound.audioSource.volume;
        while(elapsedTime<delay)
        {
            elapsedTime += Time.deltaTime;
            currentSound.audioSource.volume = Mathf.Lerp(currentVolume, 0f, elapsedTime / delay);

            if (currentSound.audioSource.volume <= 0f)
                currentSound.audioSource.Pause();


            yield return null;

        }
    }
}
