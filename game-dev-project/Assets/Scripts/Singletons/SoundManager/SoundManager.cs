using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => instance;
    private static SoundManager instance;

    [SerializeField] Music[] musics;
    [SerializeField] GameSound[] gameSounds;
    [SerializeField] UISound[] uiSounds;
    [SerializeField] AudioSource audioSource2D;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource audioSourceTemplate;
    [SerializeField] int poolSize;
    [SerializeField] Transform parent;
    
    public bool IsMusicEnabled
    {
        get => isMusicEnabled;
        set
        {
            isMusicEnabled = value;
            musicAudioSource.volume = value ? 0.6f : 0;
        }
    }

    public bool IsSFXEnabled
    {
        get => isSFXEnabled;
        set
        {
            isSFXEnabled = value;
            audioSource2D.volume = value ? 0.6f : 0;
            foreach (var x in spawnedAudioSourceUsed)
            {
                x.Stop();
            }
        }
    }

    public float MusicVolume => musicAudioSource.volume;
    public float SFXVolume => audioSource2D.volume;
    private bool isMusicEnabled = true;
    private bool isSFXEnabled  = true;
    
    private List<AudioSource> spawnedAudioSourcePool = new ();
    private List<AudioSource> spawnedAudioSourceUsed = new ();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.6f);
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.6f);
        }
        else
        {
            Load();
        }
        
        foreach (var audioSource in spawnedAudioSourceUsed)
        {
            audioSource.Stop();
        }

        for (int i = 0; i < poolSize; i++)
        {
            var x = Instantiate(audioSourceTemplate, parent);
            spawnedAudioSourcePool.Add(x);
        }
    }


    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value;
        Save(musicAudioSource.volume, audioSource2D.volume);
    }
    
    public void SetSFXVolume(float value)
    {
        audioSource2D.volume = value;
        Save(musicAudioSource.volume, audioSource2D.volume);
    }
    
    private void Save(float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }
    
    public void PlayMusic(MusicType musicType)
    {
        var clips = GetClipsBySoundType(musicType);
        var clip = clips[Mathf.FloorToInt(Random.Range(0, clips.Length))];
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    private AudioClip[] GetClipsBySoundType(MusicType musicType)
    {
        return musics.ToList().Where(x => x.musicType == musicType).SelectMany(x => x.clips).ToArray();
    }

    private void Load()
    {
        musicAudioSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.6f);
        audioSource2D.volume = PlayerPrefs.GetFloat("sfxVolume", 0.6f);
    }
    
    public void SetLoop(bool isLooping)
    {
        musicAudioSource.loop = isLooping;
    }
    
    public void PlaySound(GameSoundType soundType, Vector3 pos)
    {
        if (!IsSFXEnabled)
        {
            return;
        }

        var clips = GetClipsBySoundType(soundType);

        if (clips.Length == 0)
        {
            //Debug.LogError($"No assets to play for this Sound {soundType}");
            return;
        }

        var clip = clips[Mathf.FloorToInt(Random.Range(0, clips.Length))];

        AudioSource entry = null;
        if (spawnedAudioSourcePool.Count > 0)
        {
            entry = spawnedAudioSourcePool[0];
            spawnedAudioSourcePool.RemoveAt(0);
        }
        else
        {
            entry = spawnedAudioSourceUsed[0];
            spawnedAudioSourceUsed.RemoveAt(0);
        }

        entry.volume = PlayerPrefs.GetFloat("sfxVolume", 0.6f);
        entry.transform.position = pos;
        entry.pitch = Random.Range(0.8f, 1.2f);
        entry.PlayOneShot(clip);
        spawnedAudioSourceUsed.Add(entry);

    }

    public void PlayUISound(UISoundType soundType)
    {
        if (!IsSFXEnabled)
        {
            return;
        }

        var clips = GetClipsBySoundType(soundType);

        if (clips.Length == 0)
        {
            Debug.LogError($"No assets to play for this Sound {soundType}");
            return;
        }

        AudioClip clip = clips[Mathf.FloorToInt(Random.Range(0, clips.Length))];

        audioSource2D.Stop();
        audioSource2D.PlayOneShot(clip);
    }

    private AudioClip[] GetClipsBySoundType(GameSoundType soundType)
    {
        return gameSounds.ToList().Where(x => x.gameSoundType == soundType).SelectMany(x => x.clips).ToArray();
    }

    private AudioClip[] GetClipsBySoundType(UISoundType soundType)
    {
        return uiSounds.ToList().Where(x => x.uiSoundType == soundType).SelectMany(x => x.clips).ToArray();
    }

    
    public enum UISoundType
    {
        Click,
        GameOver,
        EndLevel
    }

    public enum GameSoundType {
        Jump,
        Collect,
        Hit,
        Wind,
        Box,
        Slide,
        Climb,
        Laser
    }

    public enum MusicType
    {
        Menu,
        Gameplay,
    }
}
