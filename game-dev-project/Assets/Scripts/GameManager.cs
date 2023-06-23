using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
    [Header("Sound Managers")]
    [SerializeField] private GameMusicManager gameMusicManager;
    [SerializeField] private SoundManager soundManager;
    
    [Header("Data Manager")]
    [SerializeField] private DataManager dataManager;
    [SerializeField] private Timer timer;
    PlayerGameProgress playerGameProgress;

    
    private const string player1KeyBindingsConfKey = "PLAYER_1_BINDINGS";
    private const string player2KeyBindingsConfKey = "PLAYER_2_BINDINGS";
    private const string player1ActionMap = "Player1";
    private const string player2ActionMap = "Player2";
    private float lastTickTime = 0;
    
    private void Start() 
    {
        BindToEvents();
        playerGameProgress = dataManager.LoadData();
        uiController.SetLevelNumber(dataManager.CurrentLvl);
        uiController.SetActiveLevelComplete(false);
        uiController.SetActiveGameOverPanel(false);

        LoadKeyBindings(player1KeyBindingsConfKey, smallPlayer, player1ActionMap);
        LoadKeyBindings(player2KeyBindingsConfKey, bigPlayer, player2ActionMap);

        GameMusicManager.Music randomMusic = (GameMusicManager.Music)UnityEngine.Random.Range(0, 3);
        gameMusicManager.PlayMusic(randomMusic);
    }

    private void OnDestroy()
    {
        UnbindFromEvents();
    }

    private void BindToEvents()
    {
        bigPlayer.onPlayersDeath += HandleGameOverState;
        bigPlayer.onPlayerHit += HandlePlayerHit;
        
        smallPlayer.onPlayersDeath += HandleGameOverState;
        smallPlayer.onPlayerHit += HandlePlayerHit;

        starCollectDetector.onStarCollected += HandleStarCollecting;

        endDoor.onEnterEndDoor += HandleEndLevel;

        uiController.onBackToMenu += HandleBackToMenu;
        uiController.onNextLevelButtonClicked += HandleNextLevelButtonClicked;
        uiController.onResetLevel += HandleResetLevel;

        uiController.onResumeLevel += HandleResume;

        smallPlayer.Movement.onEscPressed += HandlePauseGame;

        timer.onTimeTick += HandleTimeTick;
    }

    private void HandleTimeTick(float currentTime)
    {
        lastTickTime = currentTime;
        uiController.UpdateTime(GetFormatedTime(currentTime));
    }

    private string GetFormatedTime(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        
        StringBuilder builder = new StringBuilder();
        builder.Append(minutes < 10 ? "0" : "");
        builder.Append(minutes);
        builder.Append(":");
        builder.Append(seconds < 10 ? "0" : "");
        builder.Append(seconds);
        return builder.ToString();
    }
    
    private void HandleResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void HandleResume()
    {
        uiController.SetActivePausePanel(false);
        bigPlayer.Movement.ForceStartPlayer();
        smallPlayer.Movement.ForceStartPlayer();
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
        soundManager.PlaySound(SoundManager.Sounds.Collect);
        uiController.IncreaseStarAmount();
    }

    private void UnbindFromEvents()
    {
        bigPlayer.onPlayersDeath -= HandleGameOverState;
        bigPlayer.onPlayerHit -= HandlePlayerHit;
        
        smallPlayer.onPlayersDeath -= HandleGameOverState;
        smallPlayer.onPlayerHit -= HandlePlayerHit;
        
        starCollectDetector.onStarCollected -= HandleStarCollecting;
        
        endDoor.onEnterEndDoor -= HandleEndLevel;
        uiController.onResumeLevel -= HandleResume;
        uiController.onBackToMenu -= HandleBackToMenu;
        uiController.onNextLevelButtonClicked -= HandleNextLevelButtonClicked;
        smallPlayer.Movement.onEscPressed -= HandlePauseGame;
        
        timer.onTimeTick += HandleTimeTick;
    }

    private void HandleEndLevel()
    {
        timer.IsCounting = false;
        
        soundManager.PlaySound(SoundManager.Sounds.EndLevel);
        dataManager.SaveData(starCollectDetector.StarsAmount, dataManager.CurrentLvl, lastTickTime);
        uiController.SetActiveLevelComplete(true, GetFormatedTime(lastTickTime), starCollectDetector.StarsAmount);

        if (dataManager.CurrentLvl == playerGameProgress.LevelsData.Count)
        {
            uiController.HandleGameComplete();
        }
        dataManager.CurrentLvl++;
    }

    private void HandlePlayerHit(Character character)
    {
        soundManager.PlaySound(SoundManager.Sounds.Hit);
        uiController.DecreasePlayersHealth(character.Type);
    }
    
    private void HandleGameOverState()
    {
        timer.IsCounting = false;
        uiController.SetActiveGameOverPanel(true);
        bigPlayer.Movement.ForceStopPlayer();
        smallPlayer.Movement.ForceStopPlayer();
    }

    private void HandlePauseGame()
    {
        uiController.SetActivePausePanel(true);
        bigPlayer.Movement.ForceStopPlayer();
        smallPlayer.Movement.ForceStopPlayer();
    }

    private void LoadKeyBindings(string confKey, Character character, string actionMap)
    {
        string rebinds = PlayerPrefs.GetString(confKey, string.Empty);

        if (!string.IsNullOrEmpty(rebinds))
        {
            character.Movement.PlayerInput.actions.FindActionMap(actionMap).LoadBindingOverridesFromJson(rebinds);
        }
    }
}
