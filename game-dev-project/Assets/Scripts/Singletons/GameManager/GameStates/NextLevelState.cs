
public class NextLevelState : LoadUnloadLevelHandlerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UnloadScene(GameManager.Instance.Data.CurrentLevel.PathToScene);
    }

    protected override void OnUnloadingLevelComplete()
    {
        base.OnUnloadingLevelComplete();
        GameManager.Instance.Data.Status = GameData.GameStatus.LoadLevel;
    }
}
