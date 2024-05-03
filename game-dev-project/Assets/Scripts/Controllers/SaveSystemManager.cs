using System;
using UnityEngine;

public class SaveSystemManager : Singleton<SaveSystemManager> {
    private string playerSaveKey;
    
    protected override void Start()
    {
        base.Start();
        playerSaveKey = GameManager.Instance.GameSettings.playerSaveKey;
    }

    public LevelData GetLastUnlockedLevel()
    {
        PlayerGameProgress playerGameProgress = LoadData();
        LevelData previousLevel = playerGameProgress.LevelsData.FindLast(level => level.IsLocked == false);
        return previousLevel;
    }

    public void SaveData(Explanation explanation, int starsAmount, int levelNumber, float completionTime)
    {
        PlayerGameProgress playerGameProgress = LoadData();
        playerGameProgress.ChangeStarsNumber(starsAmount, levelNumber);
        playerGameProgress.ChangeCompletionTime(explanation, completionTime, levelNumber);
        
        if (levelNumber + 1 < playerGameProgress.LevelsData.Count)
        {
            playerGameProgress.UnlockLevel(levelNumber + 1);
        }
        
        SaveDataToPlayerPrefs(playerGameProgress);
    }


    public PlayerGameProgress LoadData()
    {
        if (PlayerPrefs.HasKey(playerSaveKey))
        {
            PlayerGameProgress playerGameProgress = JsonUtility.FromJson<PlayerGameProgress>(PlayerPrefs.GetString(playerSaveKey));
            
            if (playerGameProgress.LevelsData.Count != 0)
            {
                return playerGameProgress;
            }
        }
        
        return ResetData();
    }
    
    public PlayerGameProgress ResetData()
    {
        PlayerGameProgress newData = new PlayerGameProgress();
        newData.InitLevels(GameManager.Instance.GameSettings.levelPaths);
        SaveDataToPlayerPrefs(newData);
        return newData;
    }

    public LevelData GetLevelByIndex(int levelIndex)
    {
        PlayerGameProgress playerGameProgress = LoadData();
        return playerGameProgress.LevelsData[levelIndex];
    }
    
    private void SaveDataToPlayerPrefs(PlayerGameProgress playerGameProgress) 
    {
        string json = JsonUtility.ToJson(playerGameProgress);
        PlayerPrefs.SetString(playerSaveKey, json);
    }
}
