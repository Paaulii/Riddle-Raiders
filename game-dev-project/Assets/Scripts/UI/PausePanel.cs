using System;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : InGamePanel
{
    public event Action onResumeLevel;
    
    [SerializeField] private Button backButton;
    [SerializeField] private Button resetLevelButton;
    [SerializeField] private Button resumeLevelButton;
    
    public override void Show()
    {
        base.Show();
        backButton.onClick.AddListener(NotifyReturnToMenu);
        resetLevelButton.onClick.AddListener(NotifyResetLevel);
        resumeLevelButton.onClick.AddListener(NotifyResumeLevel);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.onClick.RemoveListener(NotifyReturnToMenu);
        resetLevelButton.onClick.RemoveListener(NotifyResetLevel);
        resumeLevelButton.onClick.RemoveListener(NotifyResumeLevel);
    }

    private void NotifyResumeLevel()
    {
        onResumeLevel?.Invoke();
    }
}
