using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Action<Character> onPlayersDeath;
    public Action<Character> onPlayerHit;
    public Movement Movement => movement;
    [field:SerializeField] public CharacterType Type { get; private set; }
    [SerializeField] Movement movement;
    [SerializeField] HealthManager healthManager;
    
    private void Start() 
    {
        healthManager.onPlayersDeath += () =>
        {
            onPlayersDeath?.Invoke(this);
        };
        
        healthManager.onPlayerHit += () =>
        {
            SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Hit, transform.position);
            onPlayerHit?.Invoke(this);
        };

        movement.onCatch += () => SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Box, transform.position);
        movement.onJump += () =>SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Jump, transform.position);
        movement.onSlide += () => SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Slide, transform.position);
        
        movement.onStartClimb += () =>
        {
            SoundManager.Instance.SetLoop(true);
            SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Climb, transform.position);
        };
        
        movement.onStopClimb += () =>
        {
            SoundManager.Instance.SetLoop(false);
        };
    }

    public enum CharacterType 
    {
        Big,
        Small
    }
}
