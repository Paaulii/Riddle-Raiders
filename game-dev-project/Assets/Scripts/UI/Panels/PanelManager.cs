using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] Panel startPanel;
    [SerializeField] List<Panel> panels;
    Panel currentPanel;

    private void Awake() {
        foreach (var panel in panels) {
            panel.Hide();
        }
    }

    public T ShowPanel<T>() where T : Panel {
        T panel = GetChild<T>();
        ShowPanel(panel);
        return panel;
    }
    
    public Panel ShowPanel(Type type)
    {
        Panel panel = GetChild(type);
        ShowPanel(panel);
        return panel;
    }
    
    void ShowPanel(Panel panel) {
        if (currentPanel) {
            currentPanel.Hide();
        }
        
        if (panel != null) {
            panel.Show();
            currentPanel = panel;
        }
        else {
            Debug.LogError("[PANEL MANAGER] Panel to show is null!");
        }
    }
    
    private T GetChild<T>() where T : Panel {
        foreach (Panel panel in panels) {
            if (panel is T child) {
                return child;
            }
        }
        return null;
    }
    
    private Panel GetChild(Type type) {
        foreach (Panel panel in panels) {
            if (panel.GetType() == type) {
                return panel;
            }
        }
        return null;
    }
}
