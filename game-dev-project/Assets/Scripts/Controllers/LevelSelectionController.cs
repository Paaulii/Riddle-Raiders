using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private LevelItemGridGenerator levelItemGridGenerator;
    PlayerLevelProgress playerLevelProgress;
    private void Awake() 
    {
        playerLevelProgress = dataManager.LoadData();
        Debug.Log(playerLevelProgress.LevelsData.Count);
        levelItemGridGenerator.GenerateLevelItems(playerLevelProgress.LevelsData);
        levelItemGridGenerator.onSelectLevel += HandleLevelSelect;
    }

    private void OnDestroy()
    {
        levelItemGridGenerator.onSelectLevel -= HandleLevelSelect;
    }

    private void HandleLevelSelect(int levelNumber) 
    {
        if (levelNumber - 1 >= 0)
        {
            dataManager.CurrentLVL = levelNumber;
            SceneManager.LoadScene(playerLevelProgress.LevelsData[levelNumber-1].PathToScene);
        }
    }
}
