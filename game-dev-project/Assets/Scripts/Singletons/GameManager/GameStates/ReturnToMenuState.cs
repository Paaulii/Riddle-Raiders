public class ReturnToMenuState : LoadUnloadLevelHandlerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        PlayersManager.instance.DestroyPlayers();
        UnloadScene(GameManager.Instance.Data.CurrentLevel.PathToScene);
    }

    protected override void OnUnloadingLevelComplete()
    {
        base.OnUnloadingLevelComplete();
        GameManager.Instance.Data.Status = GameData.GameStatus.MainMenu;
    }
}
