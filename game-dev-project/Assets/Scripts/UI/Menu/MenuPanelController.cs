using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public event Action onExitButtonClick; 
    public event Action onStartButtonClick; 
    
    [SerializeField] private Button startGameTriggerButton;
    [SerializeField] private Button chooseLevelTriggerButton;
    [SerializeField] private Button settingsTriggerButton;
    [SerializeField] private Button creditsTriggerButton;
    [SerializeField] private Button exitGameTriggerButton;

    private void Start()
    {
        startGameTriggerButton.onClick.AddListener(NotifyStartButtonClicked);
        chooseLevelTriggerButton.onClick.AddListener(ShowLevelSelectionPanel);
        settingsTriggerButton.onClick.AddListener(ShowSettingsPanel);
        creditsTriggerButton.onClick.AddListener(ShowCreditsPanel);
        exitGameTriggerButton.onClick.AddListener(NotifyExitButtonClicked);
    }

    private void OnDestroy()
    {
        chooseLevelTriggerButton.onClick.RemoveListener(ShowLevelSelectionPanel);
        startGameTriggerButton.onClick.RemoveListener(NotifyStartButtonClicked);
        settingsTriggerButton.onClick.RemoveListener(ShowSettingsPanel);
        creditsTriggerButton.onClick.RemoveListener(ShowCreditsPanel);
        exitGameTriggerButton.onClick.RemoveListener(NotifyExitButtonClicked);
    }

    private void NotifyStartButtonClicked()
    {
        onStartButtonClick?.Invoke();
    }

    private void NotifyExitButtonClicked()
    {
        onExitButtonClick?.Invoke();
    }

    private void ShowCreditsPanel()
    {
        ShowPanel(typeof(CreditsPanel));
    }

    private void ShowSettingsPanel()
    {
        ShowPanel(typeof(SettingsPanel));
    }

    private void ShowLevelSelectionPanel()
    {
        ShowPanel(typeof(LevelSelectionPanel));
    }

    private void ShowPanel(Type type)
    {
        PanelManager.instance.ShowPanel(type);
    }

}
