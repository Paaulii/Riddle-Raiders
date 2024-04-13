
using System;

public class GameData
{
    public event Action<GameStatus> onStateChange; 

    public enum GameStatus
    {
        MainMenu,
        ExitGame,
        LoadLevel,
        Tutorial,
        InLevel,
        ResetLevel,
        StartPause,
        InPause,
        WinScreen,
        LoseScreen,
    }

    public GameStatus Status
    {
        get => gameStatus;
        set 
        {
            if (gameStatus != value) {
                onStateChange?.Invoke(value);
                gameStatus = value;

            }
        } 
    }

    private GameStatus gameStatus;
}