using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : State 
{
    private MenuPanel menuPanel;
    
    public override void OnEnter()
    {
        Time.timeScale = 1;
        menuPanel = PanelManager.instance.ShowPanel<MenuPanel>();
        menuPanel.onExitGame += HandleExitGame;
        menuPanel.onStartGame += HandleStartGame;
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Menu);
    }


    public override void OnExit()
    {
        base.OnExit();
        menuPanel.onExitGame -= HandleExitGame;
        menuPanel.onStartGame -= HandleStartGame;
    }

    private void HandleStartGame()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.LoadLevel;
    }
    
    private void HandleExitGame() 
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ExitGame;
    }
}
