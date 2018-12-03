using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SimpleSingleton<AudioManager> {

    AudioSource[] sources;
    Dictionary<string, AudioClip> audioLibrary = new Dictionary<string, AudioClip>();

    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();
    [SerializeField]
    private List<string> clipNames = new List<string>();

    public override void Awake()
    {
        base.Awake();
        sources = GetComponentsInChildren<AudioSource>();
        for(int i = 0; i < clips.Count; ++i)
        {
            if(i > clipNames.Count - 1)
            {
                audioLibrary.Add("needName" + i, clips[i]);
            }
            else
            {
                audioLibrary.Add(clipNames[i], clips[i]);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}


    
    public static void PlaySFX(AudioClip clip, float volume = 1f, float delay = 0f)
    {
        for(int i = 0; i < Instance.sources.Length; ++i)
        {
            var s = Instance.sources[i];
            if (!s.isPlaying)
            {
                s.clip = clip;
                s.volume = volume;
                s.Play();
            }
            else if(i == Instance.sources.Length - 1)
            {
                // stop and use first source if the rest are busy
                s = Instance.sources[0];
                s.Stop();
                s.clip = clip;
                s.volume = volume;
                s.Play();
            }
        }
    }

    public static void PlaySFX(string clipName, float volume = 1f, float delay = 0f)
    {
        var clip = Instance.audioLibrary[clipName];
        if (clip == null)
        {
            Debug.LogError("No clip found with name: " + clipName);
            return;
        }
        for (int i = 0; i < Instance.sources.Length; ++i)
        {
            var s = Instance.sources[i];
            if (!s.isPlaying)
            {
                s.clip = clip;
                s.volume = volume;
                
                s.PlayDelayed(delay);
            }
            else if (i == Instance.sources.Length - 1)
            {
                // stop and use first source if the rest are busy
                s = Instance.sources[0];
                s.Stop();
                s.clip = clip;
                s.volume = volume;
                s.PlayDelayed(delay);
            }
        }
    }
}
