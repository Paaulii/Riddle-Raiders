using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGridItem : MonoBehaviour 
{
    public Action<int> onLevelItemClicked;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI completionText;
    [SerializeField] private CollectedStarsDisplayer starsDisplayer;
    [SerializeField] private Image lockedLevelCover;
    [SerializeField] private Button button;
    private int levelNumber;
    
    private void Awake()
    {
        button.onClick.AddListener(NotifyLevelItemSelected);
        starsDisplayer.ResetStars();
    }
    
    private void OnDestroy()
    {
        button.onClick.RemoveListener(NotifyLevelItemSelected);
    }
    
    public void Init(LevelData levelData)
    {
        levelNumber = levelData.LevelNumber;
        levelNumberText.text = (levelNumber + 1).ToString();
        starsDisplayer.SetCollectedStars(levelData.StarsAmount);
        completionText.text = TimerUtils.GetFormatedTime(levelData.CompletionTime);
        
        if (levelData.IsLocked)
        {
            lockedLevelCover.gameObject.SetActive(true);
            button.enabled = false;
        }
    }
    
    private void NotifyLevelItemSelected()
    {
        onLevelItemClicked?.Invoke(levelNumber);
    }
}
