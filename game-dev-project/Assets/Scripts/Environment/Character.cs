using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Action<Character> onPlayersDeath;
    public Action<Character> onPlayerHit;
    public Action<Character> onSpikeHit;
    public Action<Character> onCatch;
    public Action<Character> onJump;
    public Action<Character> onSlide;
    public Action<Character> onStartClimb;
    public Action<Character> onStopClimb;
    
    [field:SerializeField] public CharacterType Type { get; private set; }
    [SerializeField] Movement movement;
    [SerializeField] HealthManager healthManager;
    
    private void Start()
    {
        healthManager.onSpikeHit += NotifySpikeHit;
        healthManager.onPlayersDeath += NotifyOnDeath;
        healthManager.onPlayerHit += NotifyOnHit;
        movement.onCatch += NotifyOnCatch; 
        movement.onJump += NotifyOnJump;
        movement.onSlide += NotifyOnSlide;
        movement.onStartClimb += NotifyOnStartClimb;
        movement.onStopClimb += NotifyOnStopClimb;
    }

    private void OnDestroy()
    {
        healthManager.onSpikeHit -= NotifySpikeHit;
        healthManager.onPlayersDeath -= NotifyOnDeath;
        healthManager.onPlayerHit-= NotifyOnHit;
        movement.onCatch -= NotifyOnCatch; 
        movement.onJump -= NotifyOnJump;
        movement.onSlide -= NotifyOnSlide;
        movement.onStartClimb -= NotifyOnStartClimb;
        movement.onStopClimb -= NotifyOnStopClimb;
    }
    
    private void NotifyOnCatch()
    {
        onCatch?.Invoke(this);
    }

    private void NotifyOnStopClimb()
    {
       onStopClimb?.Invoke(this);
    }

    private void NotifyOnStartClimb()
    {
        onStartClimb?.Invoke(this);
    }

    private void NotifyOnSlide()
    {
        onSlide?.Invoke(this);
    }

    private void NotifyOnJump()
    {
        onJump?.Invoke(this);
    }
    
    private void NotifyOnHit()
    {
        onPlayerHit?.Invoke(this);
    }

    private void NotifyOnDeath()
    {
        onPlayersDeath?.Invoke(this);
    }

    private void NotifySpikeHit()
    {
        onSpikeHit?.Invoke(this);
    }
    
    public void ForceStopMovement()
    {
        movement.ForceStopPlayer();
    }

    public void ForceStartMovement()
    {
        movement.ForceStartPlayer();
    }
    
    public enum CharacterType 
    {
        Big,
        Small
    }
}
