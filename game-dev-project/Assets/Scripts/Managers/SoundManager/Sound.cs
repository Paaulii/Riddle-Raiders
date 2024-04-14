using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioClip[] clips;
}

[Serializable]
public class UISound : Sound
{
    public SoundManager.UISoundType uiSoundType; 
}

[Serializable]
public class GameSound : Sound
{
    public SoundManager.GameSoundType gameSoundType; 
}

[Serializable]
public class Music : Sound
{
    public SoundManager.MusicType musicType; 
}