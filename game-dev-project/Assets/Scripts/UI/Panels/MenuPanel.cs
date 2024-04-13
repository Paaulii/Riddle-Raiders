
using System;
using UnityEngine;

public class MenuPanel : Panel
{
    public Action onExitGame;
    public Action onStartGame;
    [SerializeField] MenuPanelController panelController;

    private void Start()
    {
        panelController.onExitButtonClick += NotifyExitGame;
        panelController.onStartButtonClick += NotifyStartGame;
    }
    private void OnDestroy()
    {
        panelController.onExitButtonClick -= NotifyExitGame;
        panelController.onStartButtonClick -= NotifyStartGame;
    }

    private void NotifyStartGame()
    {
        onStartGame?.Invoke();
    }
    
    private void NotifyExitGame()
    {
        onExitGame?.Invoke();
    }
}
