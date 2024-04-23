using System;
using UnityEngine;

public class TutorialPanel : Panel
{
    public Action onCloseTutorial;

    [SerializeField] private TutorialPanelController tutorialPanelController;
    
    public override void Show()
    {
        base.Show();
        tutorialPanelController.onPanelClose += NotifyCloseTutorial;
        tutorialPanelController.ShowFirstPage();
    }

    public override void Hide()
    {
        base.Hide();
        tutorialPanelController.onPanelClose -= NotifyCloseTutorial;
    }

    private void NotifyCloseTutorial()
    {
        onCloseTutorial?.Invoke();
    }
}
