using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public Sound[] sfxSounds;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySFX(string name, bool isLooping = false)
    {
        Sound s = Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else if (isLooping)
        {
            sfxSource.loop = true;
            sfxSource.clip = s.Audio;
            sfxSource.Play();
        }
        else
        {
            sfxSource.PlayOneShot(s.Audio);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
