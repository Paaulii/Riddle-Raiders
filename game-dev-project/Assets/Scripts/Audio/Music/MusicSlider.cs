using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour 
{
    private void Start()
    {
        Slider volumeSlider = GetComponent<Slider>();
        volumeSlider.value = SoundManager.Instance.MusicVolume;
        
        if (volumeSlider.value == 0)
        {
            volumeSlider.interactable = false;
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }
}
