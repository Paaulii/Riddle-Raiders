using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public event Action<int> onLevelSelected;
    public event Action onResetData;
    
    [SerializeField] private LevelItemGridGenerator levelItemGridGenerator;
    [SerializeField] private Button resetDataButton;
    
    private void Start() 
    {
        levelItemGridGenerator.onLevelSelected += HandleLevelSelect;
        resetDataButton.onClick.AddListener(NotifyResetData);
    }
    
    private void OnDestroy()
    {
        levelItemGridGenerator.onLevelSelected -= HandleLevelSelect;
        resetDataButton.onClick.RemoveListener(NotifyResetData);
    }

    public void Init(List<LevelData> levelsData)
    {
        levelItemGridGenerator.GenerateLevelItems(levelsData);
    }
    
    private void NotifyResetData()
    {
        onResetData?.Invoke();
    }
    
    private void HandleLevelSelect(int levelNumber) 
    {
        onLevelSelected?.Invoke(levelNumber);
    }
}
