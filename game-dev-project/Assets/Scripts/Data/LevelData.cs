using System;
using UnityEngine;

[Serializable]
public class LevelData 
{
    public string PathToScene => pathToScene;
    public int StarsAmount {
        get { 
            return starsAmount;
        }
        set {
            starsAmount = value;
        }
    }

    public int LevelNumber => levelNumber;
    public bool IsLocked => isLocked;
    
    [SerializeField] private string pathToScene;
    [SerializeField] private int levelNumber;
    [SerializeField] private int starsAmount;
    [SerializeField] private bool isLocked;

    public LevelData(string pathToScene, int levelNumber, int starsAmount, bool isLocked) 
    {
        this.pathToScene = pathToScene;
        this.levelNumber = levelNumber;
        this.starsAmount = starsAmount;
        this.isLocked = isLocked;
    }

    public LevelData(LevelData levelToCopy) 
    {
        pathToScene = levelToCopy.pathToScene;
        levelNumber = levelToCopy.levelNumber;
        starsAmount =  levelToCopy.starsAmount;
        isLocked =  levelToCopy.isLocked;
    }

    public void UnlockLevel() {
        isLocked = false;
    }
}
