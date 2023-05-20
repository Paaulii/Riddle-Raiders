using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteDisplayer : MonoBehaviour
{
    public Action onBackButtonClicked;
    public Action onNextLevelButtonClicked;
    [SerializeField] private CollectedStarsDisplayer starsDisplayer;
    [SerializeField] private UnityEngine.UI.Button backButton;
    [SerializeField] private UnityEngine.UI.Button nextLevelButton;

    private void Start() 
    {
        starsDisplayer.SetCollectedStars(0);

        backButton.onClick.AddListener(() =>
        {
            onBackButtonClicked?.Invoke();
        });
        
        nextLevelButton.onClick.AddListener(() =>
        {
            onNextLevelButtonClicked?.Invoke();
        });
    }

    public void SetCollectedStars(int number)
    {
        starsDisplayer.SetCollectedStars(number);
    }

    public void HandleGameComplete()
    {
        nextLevelButton.gameObject.SetActive(false);
    }
}
