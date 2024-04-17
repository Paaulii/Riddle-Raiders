public class LoadLevelState : LoadLevelHandlerState
{
    public override void OnEnter()
    {
        LoadLastLevel();
    }

    private void LoadLastLevel()
    {
        string levelName = SaveSystemManager.instance.GetLastLevelName();

        if (levelName == default)
        {
            return;
        }

        StartCoroutine(LoadSceneAsync(levelName));
    }

    protected override void OnLoadingLevelComplete()
    {
        base.OnLoadingLevelComplete();
        PlayersManager.instance.SpawnPlayers();
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}

