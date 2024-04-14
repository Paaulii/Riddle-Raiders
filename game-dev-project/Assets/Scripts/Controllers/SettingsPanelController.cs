using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button resetDataButton;
    [SerializeField] private UnityEngine.UI.Button saveButton;
    [SerializeField] private PlayerKeys bigPlayerKeys;
    [SerializeField] private PlayerKeys smallPlayerKeys;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveKeyBindings);
        resetDataButton.onClick.AddListener(ResetKeyBindings);
    }

    private void OnDestroy()
    {
        saveButton.onClick.RemoveListener(SaveKeyBindings);
        resetDataButton.onClick.RemoveListener(ResetKeyBindings);
    }
    
    private void SaveKeyBindings()
    {
        KeyBindingsManager.instance.SaveKeyBindings();
    }
    
    private void ResetKeyBindings()
    {
        KeyBindingsManager.instance.ResetKeyBindings();
        RefreshKeyContent();
    }

    public void RefreshKeyContent()
    {
        KeyBindingsManager.instance.LoadKeyBindings();
        bigPlayerKeys.RefreshKeys();
        smallPlayerKeys.RefreshKeys();
    }
}
