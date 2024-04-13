using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : State
{
    [SerializeField] private DataManager dataManager;
    public override void OnEnter() 
    {
        LoadLastLevel();
    }
    
    private void LoadLastLevel()
    {
        PlayerGameProgress playerGameProgress = dataManager.LoadData();
        LevelData lastPlayedLevel = playerGameProgress.LevelsData.FindLast(level => level.IsLocked == false);

        if (lastPlayedLevel != null)
        {
            dataManager.CurrentLvl = lastPlayedLevel.LevelNumber + 1;
            SceneManager.LoadScene(lastPlayedLevel.PathToScene);
        }
    }
}
