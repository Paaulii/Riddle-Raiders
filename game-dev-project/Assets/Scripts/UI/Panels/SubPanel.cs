using System;
using UnityEngine;
using UnityEngine.UI;

public class SubPanel : Panel
{
    [SerializeField] private Button backToMenuButton;

    protected override void Start()
    {
        base.Start();
        backToMenuButton.onClick.AddListener(GoBackToMenu);
    }

    protected override void OnDestroy()
    {
        base.Start();
        backToMenuButton.onClick.RemoveListener(GoBackToMenu);
    }

    protected void GoBackToMenu()
    {
        PanelManager.instance.ShowPanel<MenuPanel>();
    }
}
