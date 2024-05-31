using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource[] audioSources;

    public int isMute = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetVol(isMute);
    }

    public void SetVol(int vol)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = vol;
            audioSources[i].mute = vol == 0 ? true : false;
        }
    }

    public void PlayOneShot(int index)
    {
        if (index < audioSources.Length)
        {
            audioSources[index].PlayOneShot(audioSources[index].clip);
        }
    }

    public void PlayMusic(int index)
    {
        if (index < audioSources.Length)
        {
            audioSources[index].Play();
        }
    }
}
