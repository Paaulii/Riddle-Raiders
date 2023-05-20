using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private LevelItemGridGenerator levelItemGridGenerator;
    PlayerGameProgress playerGameProgress;
    private void Awake() 
    {
        playerGameProgress = dataManager.LoadData();
        Debug.Log(playerGameProgress.LevelsData.Count);
        levelItemGridGenerator.GenerateLevelItems(playerGameProgress.LevelsData);
        levelItemGridGenerator.onSelectLevel += HandleLevelSelect;
    }

    private void OnDestroy()
    {
        levelItemGridGenerator.onSelectLevel -= HandleLevelSelect;
    }

    private void HandleLevelSelect(int levelNumber) 
    {
        if (levelNumber + 1 >= 0)
        {
            dataManager.CurrentLvl = levelNumber + 1;
            SceneManager.LoadScene(playerGameProgress.LevelsData[levelNumber].PathToScene);
        }
    }
}
