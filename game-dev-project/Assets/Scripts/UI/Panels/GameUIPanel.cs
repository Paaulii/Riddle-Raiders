using System;
using UnityEngine;

public class GameUIPanel : Panel
{
    public event Action onCloseTutorial; 
    [SerializeField] private UIController controller;

    public override void Show()
    {
        base.Show();
        controller.SetLevelNumber(GameManager.Instance.Data.CurrentLvl + 1);
        controller.onCloseTutorial += NotifyTutorialClosed;
    }

    public override void Hide()
    {
        base.Hide();
        controller.onCloseTutorial -= NotifyTutorialClosed;
    }

    public void OpenTutorialPanel()
    {
        controller.OpenTutorialPanel();
    }
    
    private void NotifyTutorialClosed()
    {
        onCloseTutorial?.Invoke();
    }
}
