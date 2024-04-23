public class LoseScreenState : State
{
    private GameOverPanel panel;
    public override void OnEnter()
    {
        base.OnEnter();
        Timer.instance.StopTimer();
        panel = PanelManager.instance.ShowAdditionalPanel<GameOverPanel>();
        panel.onResetLevel += HandleResetLevel;
        panel.onReturnToMenu += ReturnToMenu;
        PlayersManager.instance.ForceStopPlayersMovement();
    }

    public override void OnExit()
    {
        base.OnExit();
        panel.onResetLevel -= HandleResetLevel;
        panel.onReturnToMenu -= ReturnToMenu;
        panel.Hide();
    }

    private void ReturnToMenu()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.MainMenu;
    }

    private void HandleResetLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ResetLevel;
    }
}
