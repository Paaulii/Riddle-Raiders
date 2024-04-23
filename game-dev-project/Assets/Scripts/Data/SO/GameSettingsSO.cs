using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    public int LevelCount => levelPaths.Length;
    public string player1KeyBindingsConfKey = "player_1_bindings";
    public string player2KeyBindingsConfKey = "player_2_bindings";
    public string player1ActionMap = "Player1";
    public string player2ActionMap = "Player2";
    public string[] levelPaths;
    public string playerSaveKey = "playerSave";
    
    [Header("Binding")]
    public KeyCode PauseButton = KeyCode.Escape;

    [Header("Level complete notices")] 
    public ExplanationNotice[] notices;

    public ExplanationNotice GetNoticeByExplanation(Explanation explanation)
    {
        return notices.FirstOrDefault(notice => notice.explanation == explanation);
    }
    
    [Serializable]
    public class ExplanationNotice
    {
        public string header;
        public string notice;
        public Explanation explanation;
    }
}