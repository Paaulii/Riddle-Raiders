using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class PlayerLevelProgress
{
    public List<LevelData> LevelsData => levelsData;
    [SerializeField] private List<LevelData> levelsData;

    public PlayerLevelProgress()
    {
        levelsData = new List<LevelData>();
    }
    
    public PlayerLevelProgress(List<LevelData> levelsData)
    {
        this.levelsData = levelsData;
    }
    
    public void InitLevels(string[] levelPaths)
    {
        levelsData = new List<LevelData>();
        for (int i = 0; i < levelPaths.Length; i++)
        {
            levelsData.Add(new LevelData(levelPaths[i], i + 1, 0, i != 0));
        }
    }

    public void ChangeLevelData(LevelData levelData, int levelNumber) {
        levelsData[levelNumber] = new LevelData(levelData);
    }
}
