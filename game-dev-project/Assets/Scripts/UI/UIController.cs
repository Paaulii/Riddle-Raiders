using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action onNextLevelButtonClicked;
    public Action onBackToMenu;
    public Action onResetLevel;
    public Action onResumeLevel;

    [Header("UI objects")] 
    
    [Header("Top info")]
    [SerializeField] private PlayerStateController smallPlayerStateController;
    [SerializeField] private PlayerStateController bigPlayerStateController;
    [SerializeField] private StarDisplayer starDisplayer;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    [Header("Panels")]
    [SerializeField] private LevelCompleteDisplayer levelCompleteDisplayer;
    [SerializeField] private GameOverPanelDisplayer gameOverPanelDisplayer;
    [SerializeField] private PauseGame pausePanelDisplayer;
    private void Start()
    {
        SetActiveGameOverPanel(false);
        SetActivePausePanel(false);
        levelCompleteDisplayer.onNextLevelButtonClicked += () => { onNextLevelButtonClicked?.Invoke(); };
        levelCompleteDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
        gameOverPanelDisplayer.onResetLevel += () => { onResetLevel?.Invoke(); };
        gameOverPanelDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
        pausePanelDisplayer.onResetLevel += () => { onResetLevel?.Invoke(); };
        pausePanelDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
        pausePanelDisplayer.onResumeLevel += () => { onResumeLevel?.Invoke(); };
    }

    public void UpdateTime(string time)
    {
        timeText.text = time;
    }
    
    public void DecreasePlayersHealth(Character.CharacterType characterType)
    {
        switch (characterType)
        {
            case Character.CharacterType.Big:
                bigPlayerStateController.DecreaseHeartsAmount();
                break;
            case Character.CharacterType.Small:
                smallPlayerStateController.DecreaseHeartsAmount();
                break;
        }
    }

    public void SetActiveGameOverPanel(bool isActive)
    {
        gameOverPanelDisplayer.gameObject.SetActive(isActive);
    }

    public void SetActivePausePanel(bool isActive)
    {
        pausePanelDisplayer.gameObject.SetActive(isActive);
    }

    public void SetActiveLevelComplete(bool isActive,  string completionTime = "", int collectedStars = 0)
    {
        levelCompleteDisplayer.gameObject.SetActive(isActive);
        levelCompleteDisplayer.SetCollectedStars(collectedStars);
        levelCompleteDisplayer.SetCompletionTime(completionTime);
    }

    public void IncreaseStarAmount()
    {
        starDisplayer.ChangeStarsNumber();
    }

    public void HandleGameComplete()
    {
        levelCompleteDisplayer.HandleGameComplete();
    }

    public void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Level " + levelNumber;
    }
}
