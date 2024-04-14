using UnityEngine;

public class SettingsPanel : SubPanel
{
    [SerializeField] private SettingsPanelController panelController;
    
    public override void Show()
    {
        base.Show();
        panelController.RefreshKeyContent();
    }
}
