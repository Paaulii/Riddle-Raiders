using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelController : MonoBehaviour
{
    public Action onPanelClose;
    [Header("Panels")] 
    [SerializeField] private List<GameObject> panels;

    [Header("Button")]
    [SerializeField] private UnityEngine.UI.Button leftButton;
    [SerializeField] private UnityEngine.UI.Button rightButton;
    [SerializeField] private UnityEngine.UI.Button closeButton;

    int currentIndexPanel;
    
    private void Start()
    {
        closeButton.onClick.AddListener(NotifyTutorialClosed);
        leftButton.onClick.AddListener(SetActivePreviousPanel);
        rightButton.onClick.AddListener(SetActiveNextPanel);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(NotifyTutorialClosed);
        leftButton.onClick.RemoveListener(SetActivePreviousPanel);
        rightButton.onClick.RemoveListener(SetActiveNextPanel);
    }
    
    public void ShowFirstPage()
    {
        currentIndexPanel = 0;
        ShowCurrentIndexPanel();
    }
    
    private void NotifyTutorialClosed()
    {
        onPanelClose?.Invoke();
    }

    private void SetActiveNextPanel()
    {
        currentIndexPanel++;
        ShowCurrentIndexPanel();
    }
    
    private void SetActivePreviousPanel()
    {
        currentIndexPanel--;
        ShowCurrentIndexPanel();
    }
    
    private void ShowCurrentIndexPanel()
    {
        SetActivePanel(currentIndexPanel);
        RefreshButtons();
    }

    private void RefreshButtons()
    {
        leftButton.gameObject.SetActive(currentIndexPanel - 1 >= 0);
        rightButton.gameObject.SetActive(currentIndexPanel + 1 < panels.Count);
    }
    
    private void SetActivePanel(int index)
    {
        SetActivePanels(false);
        panels[index].SetActive(true);
    }
    
    private void SetActivePanels(bool isActive) {
        panels.ForEach(panel => panel.SetActive(isActive));
    }
}
