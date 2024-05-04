
public class LoadLevelState : LoadUnloadLevelHandlerState
{
    public override void OnEnter()
    {
        LevelData levelToLoad = GameManager.Instance.Data.LastStatus switch {
            GameData.GameStatus.ResetLevel or GameData.GameStatus.LevelSelection => GameManager.Instance.Data.CurrentLevel,
            GameData.GameStatus.MainMenu => SaveSystemManager.instance.GetLastUnlockedLevel(),
            _ => SaveSystemManager.instance.GetLevelByIndex(GameManager.Instance.Data.CurrentLevel.LevelNumber + 1)
        };
        GameManager.Instance.Data.CurrentLevel = levelToLoad;
        LoadScene(levelToLoad.PathToScene);
    }

    protected override void OnLoadingLevelComplete()
    {
        base.OnLoadingLevelComplete();
        PlayersManager.instance.SpawnPlayers();
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}

