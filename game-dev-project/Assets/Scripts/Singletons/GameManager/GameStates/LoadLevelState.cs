
public class LoadLevelState : LoadUnloadLevelHandlerState
{
    public override void OnEnter()
    {
        string sceneToLoad = GameManager.Instance.Data.LastStatus == GameData.GameStatus.ResetLevel ? 
            GameManager.Instance.Data.CurrentLevel.PathToScene : GetLevelData().PathToScene;
        LoadScene(sceneToLoad);
    }

    protected override void OnLoadingLevelComplete()
    {
        base.OnLoadingLevelComplete();
        PlayersManager.instance.SpawnPlayers();
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
    
    private LevelData GetLevelData()
    {
        LevelData data = SaveSystemManager.instance.GetLastUnlockedLevel();
        GameManager.Instance.Data.CurrentLevel = data;
        return data;
    }
}

