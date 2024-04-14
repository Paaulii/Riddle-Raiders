using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameSettings")]
public class GameSettingsSO : ScriptableObject {
    public string player1KeyBindingsConfKey = "PLAYER_1_BINDINGS";
    public string player2KeyBindingsConfKey = "PLAYER_2_BINDINGS";
    public string player1ActionMap = "Player1";
    public string player2ActionMap = "Player2";
}