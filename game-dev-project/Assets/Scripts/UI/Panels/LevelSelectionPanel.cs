using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPanel : SubPanel
{
    public event Action onResetData;
    public event Action<int> onLevelSelected;
    public event Action onReturnToMenu;
    [SerializeField] private LevelSelectionController controller;

    public override void Show()
    {
        base.Show();
        controller.onLevelSelected += NotifyLevelSelected;
        controller.onResetData += NotifyResetData;
    }

    public override void Hide()
    {
        base.Hide();
        controller.onLevelSelected -= NotifyLevelSelected;
        controller.onResetData -= NotifyResetData;
    }

    protected override void ReturnToMenu()
    {
        base.ReturnToMenu();
        onReturnToMenu?.Invoke();
    }

    public void InitGridForData(List<LevelData> levelsData)
    {
        controller.Init(levelsData);   
    }
    
    private void NotifyResetData()
    {
        onResetData?.Invoke();
    }

    private void NotifyLevelSelected(int levelNumber)
    {
        onLevelSelected?.Invoke(levelNumber);
    }
}
