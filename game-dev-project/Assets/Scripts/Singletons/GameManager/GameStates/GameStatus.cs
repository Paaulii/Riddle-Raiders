using System;
using UnityEngine;

[Serializable]
public class GameStatus
{ 
   [SerializeField] public GameData.GameStatus status; 
   [SerializeField] public State state;
}
