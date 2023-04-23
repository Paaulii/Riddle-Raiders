using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field:SerializeField] public CharacterType Type { get; private set; }
    [SerializeField] Movement movement;
    
    public enum CharacterType 
    {
        Big,
        Small
    }
}
