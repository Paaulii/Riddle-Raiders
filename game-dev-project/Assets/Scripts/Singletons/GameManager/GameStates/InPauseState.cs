using UnityEngine;

public class InPauseState : State
{
    private PausePanel panel;
    
    public override void OnEnter()
    {
        base.OnEnter();
        StopGame();
        ShowPausePanel();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (Input.GetKeyDown(GameManager.Instance.GameSettings.PauseButton))
        {
            GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        panel.onResumeLevel -= ResumeLevel;
        panel.onResetLevel -= ResetLevel;
        panel.onReturnToMenu -= ReturnToMenu;
        panel.Hide();
    }

    private static void StopGame()
    {
        Timer.instance.StopTimer();
        PlayersManager.instance.ForceStopPlayersMovement();
    }

    private void ShowPausePanel()
    {
        panel = PanelManager.instance.ShowAdditionalPanel<PausePanel>();
        panel.onResumeLevel += ResumeLevel;
        panel.onResetLevel += ResetLevel;
        panel.onReturnToMenu += ReturnToMenu;
    }

    private void ReturnToMenu()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ReturnToMenu;
    }

    private void ResetLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ResetLevel;
    }

    private void ResumeLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}
