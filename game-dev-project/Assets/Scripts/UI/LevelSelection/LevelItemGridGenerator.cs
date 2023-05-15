using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemGridGenerator : MonoBehaviour
{
    public Action<int> onSelectLevel;
    
    [SerializeField] private LevelItem levelItemPrefab;
    public void GenerateLevelItems(List<LevelData> levelsData)
    {
        foreach (var levelData in levelsData)
        {
            LevelItem newLevelItem = Instantiate(levelItemPrefab);
            newLevelItem.transform.parent = gameObject.transform;
            newLevelItem.SetData(levelData.StarsAmount, levelData.LevelNumber, levelData.IsLocked);
            newLevelItem.onLevelClicked += (levelItem) =>
            {
                onSelectLevel?.Invoke(levelItem.LevelNumber);
            };
        }
    }
}
