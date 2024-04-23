using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : InGamePanel
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button resetLevelButton;

    public override void Show()
    {
        base.Show();
        backButton.onClick.AddListener(NotifyReturnToMenu);
        resetLevelButton.onClick.AddListener(NotifyResetLevel);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.onClick.RemoveListener(NotifyReturnToMenu);
        resetLevelButton.onClick.RemoveListener(NotifyResetLevel);
    }
}
