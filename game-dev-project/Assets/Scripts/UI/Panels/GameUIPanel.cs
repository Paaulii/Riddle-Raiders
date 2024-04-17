using System;
using UnityEngine;

public class GameUIPanel : Panel
{
    public event Action onCloseTutorial; 
    public event Action onResetLevel; 
    public event Action onBackToMenu;
    
    [SerializeField] private UIController controller;

    public override void Show()
    {
        base.Show();
        controller.SetLevelNumber(GameManager.Instance.Data.CurrentLvl + 1);
        controller.onCloseTutorial += NotifyTutorialClosed;
        controller.onResetLevel += NotifyResetGame;
        controller.onBackToMenu += NotifyBackToMenu;
    }

    public override void Hide()
    {
        base.Hide();
        controller.onCloseTutorial -= NotifyTutorialClosed;
        controller.onResumeLevel -= NotifyResetGame;
        controller.onBackToMenu -= NotifyBackToMenu;
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
    
    private void NotifyResetGame()
    {
        onResetLevel?.Invoke();
    }

    private void NotifyBackToMenu()
    {
        onBackToMenu?.Invoke();
    }

    private void NotifyTutorialClosed()
    {
        onCloseTutorial?.Invoke();
    }
}
