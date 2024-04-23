using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemGridGenerator : MonoBehaviour
{
    public Action<int> onLevelSelected;
    [SerializeField] private LevelGridItem levelGridItemPrefab;
    
    public void GenerateLevelItems(List<LevelData> levelsData)
    {
        DeleteGridElements();

        foreach (var levelData in levelsData)
        {
            LevelGridItem newLevelGridItem = Instantiate(levelGridItemPrefab, gameObject.transform, false);
            newLevelGridItem.Init(levelData);
            newLevelGridItem.onLevelItemClicked += NotifyLevelItemClicked;
        }
    }

    private void DeleteGridElements()
    {
        LevelGridItem[] gridItems = GetComponentsInChildren<LevelGridItem>(false);
        
        foreach (var gridItem in gridItems)
        {
            gridItem.onLevelItemClicked -= NotifyLevelItemClicked;
            Destroy(gridItem.gameObject);
        }
    }

    private void NotifyLevelItemClicked(int levelNumber)
    {
        onLevelSelected?.Invoke(levelNumber);
    }
}
