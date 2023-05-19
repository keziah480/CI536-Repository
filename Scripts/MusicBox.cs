using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    public AudioSource[] audioSources;
    public int currentClip;
    public int nextClip;
    public int currentSource;
    float playTime;

    private void Start()
    {
        audioSources[0].clip = clips[currentClip];
        audioSources[1].clip = clips[currentClip];
        audioSources[currentSource].Play(); 
    }

    private void Update()
    {
        
        if (nextClip == 3 && currentClip == 2 && audioSources[currentSource].time > 3.6)
        {
            audioSources[currentSource].Stop();
            currentClip = nextClip;
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        else if (nextClip == 2 && currentClip != 2)
        {
            audioSources[currentSource].Stop();
            currentClip = nextClip;
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        else if (currentClip == 2 && audioSources[currentSource].time > 3.6)
        {
            audioSources[currentSource].Stop();
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        
        if (currentClip == 3 && audioSources[currentSource].time > 7.2)
        {
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }

        if (nextClip == 1 && currentClip != 1)
        {
            audioSources[currentSource].Stop();
            currentClip = nextClip;
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        else if (currentClip == 1 && audioSources[currentSource].time > 7.2)
        {
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        if (nextClip == 0 && currentClip != 0)
        {
            audioSources[currentSource].Stop();
            currentClip = nextClip;
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
        else if (currentClip == 0 && audioSources[currentSource].time > 21.5)
        {
            currentSource += 1;
            if (currentSource > 1) currentSource = 0;
            audioSources[currentSource].clip = clips[currentClip];
            audioSources[currentSource].Play();
        }
    }

    
}
