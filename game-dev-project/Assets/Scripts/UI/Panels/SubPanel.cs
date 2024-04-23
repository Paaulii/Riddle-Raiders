using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubPanel : Panel
{
    public event Action onBackToMenu;
    [SerializeField] private Button returnToMenuButton;

    protected override void Start() 
    {
        base.Start();
        returnToMenuButton.onClick.AddListener(ReturnToMenu);
    }

    protected override void OnDestroy()
    {
        base.Start();
        returnToMenuButton.onClick.RemoveListener(ReturnToMenu);
    }

    protected virtual void ReturnToMenu()
    {
        PanelManager.instance.ShowPanel<MenuPanel>();
        onBackToMenu?.Invoke();
    }
}
