using UnityEngine;

public class TutorialState : State
{ 
    private GameUIPanel gamePanel;
    
    public override void OnEnter() 
    {
        Time.timeScale = 0;
        PlayersManager.instance.ForceStopPlayersMovement();
        OpenTutorialPanel();
        Timer.instance.IsCounting = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        gamePanel.onCloseTutorial -= ResumeGame;
    }

    private void OpenTutorialPanel()
    {
        gamePanel = PanelManager.instance.ShowPanel<GameUIPanel>();
        gamePanel.onCloseTutorial += ResumeGame;
        gamePanel.OpenTutorialPanel();
    }

    private void ResumeGame()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
    
}
