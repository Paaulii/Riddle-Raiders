using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class PlayerGameProgress
{
    public List<LevelData> LevelsData => levelsData;
    [SerializeField] private List<LevelData> levelsData;

    public PlayerGameProgress()
    {
        levelsData = new List<LevelData>();
    }
    
    public PlayerGameProgress(List<LevelData> levelsData)
    {
        this.levelsData = levelsData;
    }
    
    public void InitLevels(string[] levelPaths)
    {
        levelsData = new List<LevelData>();
        for (int i = 0; i < levelPaths.Length; i++)
        {
            levelsData.Add(new LevelData(levelPaths[i], i, 0, i != 0));
        }
    }

    public void ChangeStarsNumber(int starsAmount, int levelNumber) {
        levelsData[levelNumber].StarsAmount = starsAmount;
    }

    public void UnlockLevel(int levelNumber)
    {
        levelsData[levelNumber].UnlockLevel();
    }
}
