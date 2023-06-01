using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClip jump, endLevel, collect, hit, wind, box, slide, climb;

    AudioSource audioSource;
    Dictionary<Sounds, AudioClip> sounds;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GlobalVolumeManager.GetSoundsVolume();
        sounds = new Dictionary<Sounds, AudioClip>
        {
            { Sounds.Jump, jump },
            { Sounds.EndLevel, endLevel },
            { Sounds.Collect, collect },
            { Sounds.Hit, hit },
            { Sounds.Wind, wind },
            { Sounds.Box, box },
            { Sounds.Slide, slide },
            { Sounds.Climb, climb }
        };
        
        BindAllEnvironmentElements();
    }
    
    public void PlaySound(Sounds sound)
    {
        audioSource.volume = GlobalVolumeManager.GetSoundsVolume();
        audioSource.PlayOneShot(sounds[sound]);
    }
    
    private void BindAllEnvironmentElements()
    {
        Geyser[] geysers = FindObjectsOfType<Geyser>(true);
        foreach (var geyser in geysers) {
            geyser.onGeyserActivation += () => PlaySound(Sounds.Wind);
        }
    }
    
    public enum Sounds
    {
        Jump, EndLevel, Collect, Hit, Wind, Box, Slide, Climb
    }
}
