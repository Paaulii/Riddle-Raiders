using System;
using TMPro;
using UnityEngine;

public class SummaryPanelDisplay : MonoBehaviour
{
    public Action onResetButtonClicked;
    public Action onNextLevelButtonClicked;
    public Action onBackButtonClicked;
    
    [Header("Score prompt")]
    [SerializeField] private TextMeshProUGUI headerPromptText;
    [SerializeField] private TextMeshProUGUI explanationPromptText;

    [SerializeField] private LevelStatsItem lastTimeStats;
    [SerializeField] private LevelStatsItem currentTimeStats;
    
    [Header("Buttons")]
    [SerializeField] private UnityEngine.UI.Button backButton;
    [SerializeField] private UnityEngine.UI.Button resetLevelButton;
    [SerializeField] private UnityEngine.UI.Button nextLevelButton;
    
    private void Awake()
    {
        resetLevelButton.onClick.AddListener(NotifyResetButtonClicked);
        nextLevelButton.onClick.AddListener(NotifyNextLevelButtonClicked);
        backButton.onClick.AddListener(NotifyBackButtonClicked);
    }
    
    private void OnDestroy()
    {
        resetLevelButton.onClick.RemoveListener(NotifyResetButtonClicked);
        nextLevelButton.onClick.RemoveListener(NotifyNextLevelButtonClicked);
        backButton.onClick.RemoveListener(NotifyBackButtonClicked);
    }

    public void ShowLevelCompletionPanel(Explanation explanation, 
        int lastCollectedStars, 
        int currentCollectedStars, 
        string lastCompletionTime, 
        string currentCompletionTime)
    {
        GameSettingsSO.ExplanationNotice explanationNotice = GameManager.Instance.GameSettings.GetNoticeByExplanation(explanation);
        headerPromptText.text = explanationNotice.header;
        explanationPromptText.text = explanationNotice.notice;
        
        lastTimeStats.Setup(lastCompletionTime,lastCollectedStars);
        currentTimeStats.Setup(currentCompletionTime,currentCollectedStars);
    }

    public void HandleGameComplete()
    {
        nextLevelButton.gameObject.SetActive(false);
    }

    private void NotifyResetButtonClicked()
    {
        onResetButtonClicked?.Invoke();
    }
    
    private void NotifyNextLevelButtonClicked()
    {
        onNextLevelButtonClicked?.Invoke();
    }
    
    private void NotifyBackButtonClicked()
    {
        onBackButtonClicked?.Invoke();
    }
}
