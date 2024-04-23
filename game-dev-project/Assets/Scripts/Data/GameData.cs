
using System;
using System.Collections.Generic;

public class GameData
{
    public event Action<GameStatus> onStateChange; 

    public enum GameStatus
    {
        MainMenu,
        ExitGame,
        LevelSelection,
        LoadLevel,
        Tutorial,
        InLevel,
        InPause,
        ResetLevel,
        LoseScreen,
        SummaryScreen,
        NextLevel,
        ReturnToMenu
    }

    public LevelData CurrentLevel { get; set; }
    public GameStatus Status
    {
        get => gameStatus;
        set 
        {
            if (gameStatus != value) 
            {
                LastStatus = gameStatus;
                gameStatus = value;
                onStateChange?.Invoke(value);
            }
        } 
    }

    public GameStatus LastStatus { get; private set; } = GameStatus.MainMenu;
    private GameStatus gameStatus;
}