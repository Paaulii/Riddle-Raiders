using UnityEngine;

public class TutorialState : State
{ 
    private TutorialPanel tutorialPanel;
    
    public override void OnEnter()
    {
        StopGame();
        OpenTutorialPanel();
    }
    
    public override void OnExit()
    {
        base.OnExit();
        tutorialPanel.onCloseTutorial -= ResumeGame;
        tutorialPanel.Hide();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        Timer.instance.StopTimer();
        PlayersManager.instance.ForceStopPlayersMovement();
    }
    
    private void OpenTutorialPanel()
    {
        tutorialPanel = PanelManager.instance.ShowPanel<TutorialPanel>();
        tutorialPanel.onCloseTutorial += ResumeGame;
    }

    private void ResumeGame()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}
