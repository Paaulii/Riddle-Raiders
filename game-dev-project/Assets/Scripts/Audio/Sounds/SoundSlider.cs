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
        volumeSlider.value = SoundManager.Instance.SFXVolume;
        if (volumeSlider.value == 0)
        {
            volumeSlider.interactable = false;
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
        onChangeSoundVolume?.Invoke();
    }
}
