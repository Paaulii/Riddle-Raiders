using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Character bigPlayer;
    [SerializeField] private Character smallPlayer;

    [Header("Items")] 
    [SerializeField] private StarCollectDetector starCollectDetector;
    
    [Header("UI objects")] 
    [SerializeField] private UIController uiController;

    [Header("End level")]
    [SerializeField] private EnterEndDoor endDoor;

    [SerializeField] private DataManager dataManager;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip onEndLevel;
    PlayerGameProgress playerGameProgress;
    private void Start() 
    {
        BindToEvents();
        playerGameProgress = dataManager.LoadData();
        uiController.SetLevelNumber(dataManager.CurrentLvl);
        uiController.SetActiveLevelComplete(false);
        uiController.SetActiveGameOverPanel(false);
    }

    private void OnDestroy()
    {
        UnbindFromEvents();
    }

    private void BindToEvents()
    {
        bigPlayer.onPlayersDeath += HandleGameOverState;
        bigPlayer.onPlayerHit += DecreaseUIHearts;
        
        smallPlayer.onPlayersDeath += HandleGameOverState;
        smallPlayer.onPlayerHit += DecreaseUIHearts;

        starCollectDetector.onStarCollected += HandleStarCollecting;

        endDoor.onEnterEndDoor += HandleEndLevel;

        uiController.onBackToMenu += HandleBackToMenu;
        uiController.onNextLevelButtonClicked += HandleNextLevelButtonClicked;
        uiController.onResetLevel += HandleResetLevel;
    }

    private void HandleResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void HandleNextLevelButtonClicked()
    {
        int levelIndex = dataManager.CurrentLvl - 1;
        
        if (levelIndex >= 0) {
            SceneManager.LoadScene(playerGameProgress.LevelsData[levelIndex].PathToScene);
        }
    }

    private void HandleStarCollecting()
    {
        uiController.IncreaseStarAmount();
    }

    private void UnbindFromEvents()
    {
        bigPlayer.onPlayersDeath -= HandleGameOverState;
        bigPlayer.onPlayerHit -= DecreaseUIHearts;
        
        smallPlayer.onPlayersDeath -= HandleGameOverState;
        smallPlayer.onPlayerHit -= DecreaseUIHearts;
        
        starCollectDetector.onStarCollected -= HandleStarCollecting;
        
        endDoor.onEnterEndDoor -= HandleEndLevel;
        
        uiController.onBackToMenu -= HandleBackToMenu;
        uiController.onNextLevelButtonClicked -= HandleNextLevelButtonClicked;
    }

    private void HandleEndLevel()
    {
        dataManager.SaveData(starCollectDetector.StarsAmount, dataManager.CurrentLvl);
        uiController.SetActiveLevelComplete(true, starCollectDetector.StarsAmount);

        if (dataManager.CurrentLvl == playerGameProgress.LevelsData.Count)
        {
            uiController.HandleGameComplete();
        }
        dataManager.CurrentLvl++;
    }

    private void DecreaseUIHearts(Character character)
    {
        uiController.DecreasePlayersHealth(character.Type);
    }
    
    private void HandleGameOverState()
    {
        uiController.SetActiveGameOverPanel(true);
        bigPlayer.Movement.ForceStopPlayer();
        smallPlayer.Movement.ForceStopPlayer();
    }
}
