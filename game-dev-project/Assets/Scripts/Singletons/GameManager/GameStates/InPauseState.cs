public class InPauseState : State
{
    private GameUIPanel panel;
    
    public override void OnEnter()
    {
        base.OnEnter();
        Timer.instance.StopTimer();
        panel = PanelManager.instance.GetPanel<GameUIPanel>();
        panel.SetActivePausePanel(true);
        panel.onResumeLevel += ResumeLevel;
        panel.onResetLevel += ResetLevel;
        panel.onBackToMenu += BackToMenu;
        PlayersManager.instance.ForceStopPlayersMovement();
        
    }

    private void BackToMenu()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ReturnToMenu;
    }

    private void ResetLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ResetLevel;
    }

    public override void OnExit()
    {
        base.OnExit();
        panel.SetActivePausePanel(false);
        panel.onResumeLevel -= ResumeLevel;
    }

    private void ResumeLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}
