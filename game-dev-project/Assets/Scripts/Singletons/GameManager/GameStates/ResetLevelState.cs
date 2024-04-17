public class ResetLevelState : LoadLevelHandlerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        ResetLevel();
    }
    
    protected override void OnUnloadingLevelComplete()
    {
        base.OnUnloadingLevelComplete();
        GameManager.Instance.Data.Status = GameData.GameStatus.LoadLevel;
    }

    private void ResetLevel()
    {
        StartCoroutine(UnloadSceneAsync(GetCurrentLevelName()));
    }
    
    private string GetCurrentLevelName()
    {
        return  SaveSystemManager.instance.GetLastLevelName();
    }
}
