using System;
using UnityEngine;

[Serializable]
public class LevelData 
{
    public string PathToScene => pathToScene;
    public int StarsAmount  => starsAmount;
    public int LevelNumber => levelNumber;
    public bool IsLocked => isLocked;
    public float CompletionTime => completionTime;

    
    [SerializeField] private string pathToScene;
    [SerializeField] private int levelNumber;
    [SerializeField] private int starsAmount;
    [SerializeField] private bool isLocked;
    [SerializeField] private float completionTime;

    public LevelData(string pathToScene, int levelNumber, int starsAmount, bool isLocked, float completionTime = -1) 
    {
        this.pathToScene = pathToScene;
        this.levelNumber = levelNumber;
        this.starsAmount = starsAmount;
        this.isLocked = isLocked;
        this.completionTime = completionTime;
    }

    public LevelData(LevelData levelToCopy) 
    {
        pathToScene = levelToCopy.pathToScene;
        levelNumber = levelToCopy.levelNumber;
        starsAmount =  levelToCopy.starsAmount;
        isLocked =  levelToCopy.isLocked; 
        completionTime = levelToCopy.completionTime;
    }
    
    public LevelData(LevelData levelToCopy, float completionTime) 
    {
        pathToScene = levelToCopy.pathToScene;
        levelNumber = levelToCopy.levelNumber;
        starsAmount =  levelToCopy.starsAmount;
        isLocked =  levelToCopy.isLocked; 
        this.completionTime = completionTime;
    }
    
    public LevelData(LevelData levelToCopy, int starsAmount) 
    {
        pathToScene = levelToCopy.pathToScene;
        levelNumber = levelToCopy.levelNumber;
        isLocked =  levelToCopy.isLocked; 
        completionTime = levelToCopy.completionTime;
        this.starsAmount =  starsAmount;
    }
    
    public void UnlockLevel() {
        isLocked = false;
    }
}
