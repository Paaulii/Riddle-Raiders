using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button backToMenuButton;
    [SerializeField] private UnityEngine.UI.Button resetDataButton;
    [SerializeField] private UnityEngine.UI.Button saveButton;
    [SerializeField] private RebindKeyController rebindKeyController;
    private void Start()
    {
        backToMenuButton.onClick.AddListener(() => {
            PanelManager.instance.ShowPanel<MenuPanel>();
        });
        
        saveButton.onClick.AddListener(rebindKeyController.SaveKeyBindings);
    }

    private void OnDestroy()
    {
        saveButton.onClick.RemoveListener(rebindKeyController.SaveKeyBindings);
    }
}
