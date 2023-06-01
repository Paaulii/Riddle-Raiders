using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    public enum Music
    {
        GameMusic1
    }
    
    [Header("Music clips")]
    [SerializeField] private AudioClip gameMusic1;
    
    AudioSource audioSource;

    Dictionary<Music, AudioClip> musicClips;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GlobalVolumeManager.GetMusicVolume();
        
        musicClips = new Dictionary<Music, AudioClip> {
            { Music.GameMusic1, gameMusic1 }
        };
    }

    public void PlayMusic(Music music)
    {
        audioSource.clip = musicClips[music];
        audioSource.loop = true;
        audioSource.Play();
    }
}
