using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    [SerializeField] MovementController movementController;
    [SerializeField] HealthManager healthManager;
    [SerializeField] Animator animator;
    
    private void Start()
    {
        healthManager.onSpikeHit += NotifySpikeHit;
        healthManager.onPlayersDeath += NotifyOnDeath;
        healthManager.onPlayerHit += NotifyOnHit;
        movementController.onCatch += NotifyOnCatch; 
        movementController.onJump += NotifyOnJump;
        movementController.onSlide += NotifyOnSlide;
        movementController.onStartClimb += NotifyOnStartClimb;
        movementController.onStopClimb += NotifyOnStopClimb;
    }
    
    private void OnDestroy()
    {
        healthManager.onSpikeHit -= NotifySpikeHit;
        healthManager.onPlayersDeath -= NotifyOnDeath;
        healthManager.onPlayerHit-= NotifyOnHit;
        movementController.onCatch -= NotifyOnCatch; 
        movementController.onJump -= NotifyOnJump;
        movementController.onSlide -= NotifyOnSlide;
        movementController.onStartClimb -= NotifyOnStartClimb;
        movementController.onStopClimb -= NotifyOnStopClimb;
    }

    private void Update()
    {
        RunAnimation();
    }

    private void RunAnimation()
    {
        animator.SetBool("isMoving", movementController.IsMoving);
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
        movementController.ForceStopPlayer();
    }

    public void ForceStartMovement()
    {
        movementController.ForceStartPlayer();
    }
    
    public enum CharacterType 
    {
        Big,
        Small
    }
}
