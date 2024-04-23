
using System;
using UnityEngine;

public class MenuPanel : Panel
{
    public Action onExitGame;
    public Action onStartGame;
    public Action onEnterLevelSelection;
    [SerializeField] MenuPanelController panelController;

    private void Start()
    {
        panelController.onExitButtonClick += NotifyExitGame;
        panelController.onStartButtonClick += NotifyStartGame;
        panelController.onEnterLevelSelection += NotifyEnterLevelSelection;
    }

    private void OnDestroy()
    {
        panelController.onExitButtonClick -= NotifyExitGame;
        panelController.onStartButtonClick -= NotifyStartGame;
        panelController.onEnterLevelSelection -= NotifyEnterLevelSelection;
    }

    private void NotifyStartGame()
    {
        onStartGame?.Invoke();
    }
    
    private void NotifyExitGame()
    {
        onExitGame?.Invoke();
    }
    
    private void NotifyEnterLevelSelection()
    {
        onEnterLevelSelection?.Invoke();
    }
}
