using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public event Action<SoundManager.Sounds> PlaySound;
    private void Start()
    {
        Slider volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        GlobalVolumeManager.SetSoundsVolume(volume);
        PlaySound?.Invoke(SoundManager.Sounds.Collect);
    }
}
