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
    
    PlayerGameProgress playerGameProgress;
    private void Start() 
    {
        BindToEvents();
        playerGameProgress = dataManager.LoadData();
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

        endDoor.onEnterEndDoor += EndOfLevel;
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
        
        endDoor.onEnterEndDoor -= EndOfLevel;
    }

    private void EndOfLevel()
    {
        Debug.Log("Next Level");
        dataManager.SaveData(starCollectDetector.StarsAmount, dataManager.CurrentLvl);
        dataManager.CurrentLvl++;
        
        if (dataManager.CurrentLvl == playerGameProgress.LevelsData.Count)
        {
            HandleGameOverState();
        }
        else 
        {
            SceneManager.LoadScene(playerGameProgress.LevelsData[dataManager.CurrentLvl - 1].PathToScene);
        }
    }

    private void DecreaseUIHearts(Character character)
    {
        uiController.DecreasePlayersHealth(character.Type);
    }
    
    private void HandleGameOverState()
    {
        uiController.SetActiveGameOverText(true);
        bigPlayer.Movement.enabled = false;
        smallPlayer.Movement.enabled = false;
    }
}
