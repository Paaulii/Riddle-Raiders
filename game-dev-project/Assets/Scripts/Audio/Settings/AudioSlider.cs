using UnityEngine;
using UnityEngine.UI;

public abstract class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;
    
    [Header("Mute properties")]
    [SerializeField] Button toggleMuteButton;
    [SerializeField] Image muteButtonImage;
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite onSprite;
    
    private bool isMuted;
    private float previousVolume;
    
    protected abstract float GetAudioVolume();
    protected abstract void ChangeVolume(float volume);

    private void Start()
    {
        audioSlider.value = GetAudioVolume();
        audioSlider.onValueChanged.AddListener(ChangeVolume);
        toggleMuteButton.onClick.AddListener(ToggleMusic);

        isMuted = audioSlider.value == 0.0f;
        muteButtonImage.sprite = isMuted ? onSprite : offSprite;
    }

    private void OnDestroy()
    {
        audioSlider.onValueChanged.RemoveListener(ChangeVolume);
        toggleMuteButton.onClick.RemoveListener(ToggleMusic);
    }
    
    private void ToggleMusic()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            previousVolume = GetAudioVolume();
            ChangeVolume(0.0f);
            audioSlider.value = 0f;
            muteButtonImage.sprite = onSprite;
            audioSlider.interactable = false;
        }
        else
        {
            ChangeVolume(previousVolume);
            audioSlider.value = previousVolume;
            muteButtonImage.sprite = offSprite;
            audioSlider.interactable = true;
        }
    }
}
