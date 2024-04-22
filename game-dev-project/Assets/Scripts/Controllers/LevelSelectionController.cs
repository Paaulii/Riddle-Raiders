using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private LevelItemGridGenerator levelItemGridGenerator;
    [SerializeField] private UnityEngine.UI.Button resetDataButton;
    PlayerGameProgress playerGameProgress;
    
    private void Awake() 
    {
        playerGameProgress = SaveSystemManager.instance.LoadData();
        levelItemGridGenerator.GenerateLevelItems(playerGameProgress.LevelsData);
        levelItemGridGenerator.onSelectLevel += HandleLevelSelect;

        resetDataButton.onClick.AddListener(() =>
        {
            playerGameProgress = SaveSystemManager.instance.ResetData();
            levelItemGridGenerator.GenerateLevelItems(playerGameProgress.LevelsData);
        });
    }

    private void OnDestroy()
    {
        levelItemGridGenerator.onSelectLevel -= HandleLevelSelect;
    }

    private void HandleLevelSelect(int levelNumber) 
    {
        if (levelNumber >= 0)
        {
            //TODO: it shouldnt be like this
            //GameManager.Instance.Data.CurrentLvl = levelNumber ;
            SceneManager.LoadScene(playerGameProgress.LevelsData[levelNumber].PathToScene);
        }
    }
}
