using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Action onChangeSoundVolume;
    private void Start()
    {
        Slider volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        GlobalVolumeManager.SetSoundsVolume(volume);
        onChangeSoundVolume?.Invoke();
    }
}
