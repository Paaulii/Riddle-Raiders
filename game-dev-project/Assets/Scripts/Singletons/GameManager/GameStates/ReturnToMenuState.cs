public class ReturnToMenuState : LoadLevelHandlerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        UnloadCurrentScene();
    }

    protected override void OnUnloadingLevelComplete()
    {
        base.OnUnloadingLevelComplete();

        GameManager.Instance.Data.Status = GameData.GameStatus.MainMenu;
    }

    private void UnloadCurrentScene()
    {
        string levelName = SaveSystemManager.instance.GetLastLevelName();
        StartCoroutine(UnloadSceneAsync(levelName));
    }
}
