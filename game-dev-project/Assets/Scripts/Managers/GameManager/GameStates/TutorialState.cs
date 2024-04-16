
using UnityEngine;

public class TutorialState : State
{ 
    private GameUIPanel gamePanel;
    
    public override void OnEnter() 
    {
        Time.timeScale = 0;
        gamePanel = PanelManager.instance.ShowPanel<GameUIPanel>();
        gamePanel.onCloseTutorial += GoToInLevelState;
        gamePanel.OpenTutorialPanel();
        Timer.instance.IsCounting = false;
        
        /*bigPlayer.Movement.ForceStopPlayer();
        smallPlayer.Movement.ForceStopPlayer();*/ // PlayerManager*/
    }

    public override void OnExit()
    {
        base.OnExit();
        gamePanel.onCloseTutorial -= GoToInLevelState;
    }

    private void GoToInLevelState()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}
