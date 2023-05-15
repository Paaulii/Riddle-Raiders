using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "DataManager", menuName = "ScriptableObjects/DataManager", order = 1)]
public class DataManager : ScriptableObject {
    public int CurrentLVL { get; set; }
    string PLAYER_DATA_KEY = "playerLevelProgress";
    public string[] levelPaths;
    
    public void SaveData(LevelData levelData, int levelNumber)
    {
        PlayerLevelProgress playerLevelProgress = LoadData();
        playerLevelProgress.ChangeLevelData(levelData, levelNumber);
        SaveDataToPlayerPrefs(playerLevelProgress);
    }
    
    public PlayerLevelProgress LoadData()
    {
        PlayerLevelProgress playerLevelProgress = new PlayerLevelProgress();
        
        if (PlayerPrefs.HasKey(PLAYER_DATA_KEY))
        {
            playerLevelProgress = JsonUtility.FromJson<PlayerLevelProgress>(PlayerPrefs.GetString(PLAYER_DATA_KEY));
        }
        else 
        {
            playerLevelProgress.InitLevels(levelPaths);
            SaveDataToPlayerPrefs(playerLevelProgress);
        }

        return playerLevelProgress;
    }

    void SaveDataToPlayerPrefs(PlayerLevelProgress playerLevelProgress) 
    {
        string json = JsonUtility.ToJson(playerLevelProgress);
        PlayerPrefs.SetString(PLAYER_DATA_KEY, json);
    }
}
