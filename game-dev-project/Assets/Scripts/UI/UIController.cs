using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnNextLevelButtonClicked;
    public Action onBackToMenu;

    [Header("UI objects")] 
    
    [Header("Top info")]
    [SerializeField] private PlayerStateController smallPlayerStateController;
    [SerializeField] private PlayerStateController bigPlayerStateController;
    [SerializeField] private StarDisplayer starDisplayer;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Header("Panels")]
    [SerializeField] private LevelCompleteDisplayer levelCompleteDisplayer;
    [SerializeField] private GameObject gameOverText;

    private void Start()
    {
        SetActiveGameOverText(false);
        levelCompleteDisplayer.onNextLevelButtonClicked += () => { OnNextLevelButtonClicked?.Invoke(); };

        levelCompleteDisplayer.onBackButtonClicked += () => { onBackToMenu?.Invoke(); };
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

    public void SetActiveGameOverText(bool isActive)
    {
        gameOverText.SetActive(isActive);
    }

    public void SetActiveLevelComplete(bool isActive, int collectedStars = 0)
    {
        levelCompleteDisplayer.gameObject.SetActive(isActive);
        levelCompleteDisplayer.SetCollectedStars(collectedStars);
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
