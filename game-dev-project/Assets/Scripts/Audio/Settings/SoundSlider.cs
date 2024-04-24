
public class SoundSlider : AudioSlider
{
    protected override float GetAudioVolume()
    {
        return SoundManager.Instance.SFXVolume;
    }

    protected override void ChangeVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }
}
