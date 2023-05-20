using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelItemGridGenerator : MonoBehaviour
{
    public Action<int> onSelectLevel;
    
    [SerializeField] private LevelItem levelItemPrefab;
    public void GenerateLevelItems(List<LevelData> levelsData)
    {
        LevelItem[] childObjects = GetComponentsInChildren<LevelItem>(false);
        childObjects.ToList().ForEach(childObject => Destroy(childObject.gameObject));
        
        foreach (var levelData in levelsData)
        {
            LevelItem newLevelItem = Instantiate(levelItemPrefab);
            newLevelItem.transform.SetParent(gameObject.transform, false);
            newLevelItem.SetData(levelData.StarsAmount, levelData.LevelNumber, levelData.IsLocked);
            newLevelItem.onLevelClicked += (levelItem) =>
            {
                onSelectLevel?.Invoke(levelItem.LevelNumber);
            };
        }
    }
}
