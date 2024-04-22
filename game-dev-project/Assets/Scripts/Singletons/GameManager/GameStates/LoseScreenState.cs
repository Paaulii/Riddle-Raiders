public class LoseScreenState : State
{
    private GameUIPanel panel;
    public override void OnEnter()
    {
        base.OnEnter();
        Timer.instance.StopTimer();
        panel = PanelManager.instance.GetPanel<GameUIPanel>();
        panel.SetActiveGameOverPanel(true);
        panel.onResetLevel += HandleResetLevel;
        panel.onBackToMenu += HandleBackToMenu;
        PlayersManager.instance.ForceStopPlayersMovement();
    }

    public override void OnExit()
    {
        base.OnExit();
        panel.onResetLevel -= HandleResetLevel;
        panel.onBackToMenu -= HandleBackToMenu;
        panel.SetActiveGameOverPanel(false);
    }

    private void HandleBackToMenu()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.MainMenu;
    }

    private void HandleResetLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ResetLevel;
    }
}
