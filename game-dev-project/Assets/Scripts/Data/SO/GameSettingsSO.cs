using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameSettings")]
public class GameSettingsSO : ScriptableObject {
    public string player1KeyBindingsConfKey = "player_1_bindings";
    public string player2KeyBindingsConfKey = "player_2_bindings";
    public string player1ActionMap = "Player1";
    public string player2ActionMap = "Player2";
    public string[] levelPaths;
    public string playerSaveKey = "playerSave";
    
    [Header("Binding")]
    public KeyCode PauseButton = KeyCode.Escape;
}