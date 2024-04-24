
public class MusicSlider : AudioSlider
{
    protected override float GetAudioVolume()
    {
        return SoundManager.Instance.MusicVolume;
    }

    protected override void ChangeVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }
}
