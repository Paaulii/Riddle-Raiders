using System;
using UnityEngine;

public class SummaryLevelPanel : InGamePanel
{
    public event Action onStartNextLevel;
    
    [SerializeField] private SummaryPanelDisplay summaryPanelDisplay;

    public override void Show()
    {
        base.Show();
        summaryPanelDisplay.onResetButtonClicked += NotifyResetLevel;
        summaryPanelDisplay.onNextLevelButtonClicked += NotifyStartNextLevel;
        summaryPanelDisplay.onBackButtonClicked += NotifyReturnToMenu;
    }

    public override void Hide()
    {
        base.Hide();
        summaryPanelDisplay.onResetButtonClicked -= NotifyResetLevel;
        summaryPanelDisplay.onNextLevelButtonClicked -= NotifyStartNextLevel;
        summaryPanelDisplay.onBackButtonClicked -= NotifyReturnToMenu;
    }

    public void OpenLevelCompletePanel(Explanation explanation, int lastCollectedStars, 
        int collectedStars, string lastCompletionTime, string currentCompletionTime)
    {
        summaryPanelDisplay.gameObject.SetActive(true);
        summaryPanelDisplay.ShowLevelCompletionPanel(explanation, 
            lastCollectedStars, 
            collectedStars,
            lastCompletionTime, 
            currentCompletionTime);
    }

    public void HandleGameComplete()
    {
        summaryPanelDisplay.HandleGameComplete();
    }
    
    private void NotifyStartNextLevel()
    {
        onStartNextLevel?.Invoke();
    }
}
