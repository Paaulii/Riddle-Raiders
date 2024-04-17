using System;
using UnityEngine;

public class GameUIPanel : Panel
{
    public event Action onCloseTutorial; 
    public event Action onResetLevel;
    public event Action onBackToMenu;
    public event Action onResumeLevel;
    
    [SerializeField] private UIController controller;

    public override void Show()
    {
        base.Show();
        controller.SetLevelNumber(GameManager.Instance.Data.CurrentLvl + 1);
        controller.onCloseTutorial += NotifyTutorialClosed;
        controller.onResetLevel += NotifyResetGame;
        controller.onBackToMenu += NotifyBackToMenu;
        controller.onResumeLevel += NotifyResumeLevel;
    }

    public override void Hide()
    {
        base.Hide();
        controller.onCloseTutorial -= NotifyTutorialClosed;
        controller.onResumeLevel -= NotifyResetGame;
        controller.onBackToMenu -= NotifyBackToMenu;
        controller.onResumeLevel -= NotifyResumeLevel;
    }

    public void UpdateTime(string time)
    {
        controller.UpdateTime(time);
    }
    
    public void IncreaseStarAmount()
    {
        controller.IncreaseStarAmount();
    }
    public void OpenTutorialPanel()
    {
        controller.OpenTutorialPanel();
    }

    public void DecreasePlayersHealth(Character.CharacterType characterType)
    {
        controller.DecreasePlayersHealth(characterType);
    }

    public void SetActiveGameOverPanel(bool isActive)
    {
        controller.SetActiveGameOverPanel(isActive);
    }
    
    private void NotifyTutorialClosed()
    {
        onCloseTutorial?.Invoke();
    }
    
    private void NotifyResetGame()
    {
        onResetLevel?.Invoke();
    }

    private void NotifyBackToMenu()
    {
        onBackToMenu?.Invoke();
    }
    
    private void NotifyResumeLevel()
    {
        onResumeLevel?.Invoke();
    }

    public void SetActivePausePanel(bool isActive)
    {
        controller.SetActivePausePanel(isActive);
    }
}
