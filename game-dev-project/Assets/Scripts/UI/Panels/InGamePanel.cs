using System;

public class InGamePanel : Panel
{
    public event Action onResetLevel;
    public event Action onReturnToMenu;

    protected void NotifyResetLevel()
    {
        onResetLevel?.Invoke();
    }
    
    protected void NotifyReturnToMenu()
    {
        onReturnToMenu?.Invoke();
    }
}
