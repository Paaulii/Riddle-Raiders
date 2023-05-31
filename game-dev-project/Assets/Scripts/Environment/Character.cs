using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Action onPlayersDeath;
    public Action<Character> onPlayerHit;
    public Movement Movement => movement;
    [field:SerializeField] public CharacterType Type { get; private set; }
    [SerializeField] Movement movement;
    [SerializeField] HealthManager healthManager;
    
    private void Start() 
    {
        healthManager.onPlayersDeath += () =>
        {
            onPlayersDeath?.Invoke();
        };
        
        healthManager.onPlayerHit += () =>
        {
            onPlayerHit?.Invoke(this);
        };
    }

    public enum CharacterType 
    {
        Big,
        Small
    }
}
