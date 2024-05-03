using UnityEngine;
public class SummaryScreenState : State
{
    [SerializeField] private StarCollectDetector starCollectDetector;
    private SummaryLevelPanel panel;
    private LevelController levelController;
    
    public override void OnEnter()
    {
        base.OnEnter();
        panel = PanelManager.instance.ShowAdditionalPanel<SummaryLevelPanel>();
        levelController = FindObjectOfType<LevelController>();
        panel.onStartNextLevel += StartNextLevel;
        panel.onReturnToMenu += ReturnToMenu;
        panel.onResetLevel += ResetLevel;
        Timer.instance.StopTimer();
        SetupSummaryPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        panel.onStartNextLevel -= StartNextLevel;
        panel.onReturnToMenu -= ReturnToMenu;
        panel.onResetLevel -= ResetLevel;
        panel.Hide();
    }
    
    private void StartNextLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.NextLevel;
    }
    
    private void ReturnToMenu()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ReturnToMenu;
    }

    private void ResetLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.ResetLevel;
    }

    private void SetupSummaryPanel()
    {
        int currentLevel = GameManager.Instance.Data.CurrentLevel.LevelNumber;
        float currentTime = Timer.instance.CurrentTime;
        Explanation explanation = OpenLevelCompletePanel(currentLevel, currentTime);
        SaveSystemManager.instance.SaveData(explanation, starCollectDetector.StarsAmount, currentLevel, Timer.instance.CurrentTime);


        if (explanation == Explanation.WorseTimeEqualStars || explanation == Explanation.BetterTimeLessStars || explanation == Explanation.WorseTimeLessStars)
        {
            SoundManager.Instance.PlayUISound(SoundManager.UISoundType.GameOver);
            ParticleEffectsSystem.instance.SpawnEffect(EffectType.Smoke, levelController.EndDoorPosition + new Vector2(2, 0));
        }
        else
        {
            SoundManager.Instance.PlayUISound(SoundManager.UISoundType.EndLevel);
            ParticleEffectsSystem.instance.SpawnEffect(EffectType.Firework, levelController.EndDoorPosition + new Vector2(1, 0));
            ParticleEffectsSystem.instance.SpawnEffect(EffectType.FireworkShoot, levelController.EndDoorPosition + new Vector2(1, 0));
        }

        if (currentLevel == GameManager.Instance.GameSettings.LevelCount)
        {
            panel.HandleGameComplete();
        }
    }


    private Explanation OpenLevelCompletePanel(int levelIndex, float currentCompletionTime)
    {
        LevelData currentLevel = SaveSystemManager.instance.GetLevelByIndex(levelIndex);
        int lastCollectedStars = currentLevel.StarsAmount;
        int currentCollectedStars = starCollectDetector.StarsAmount;
        float lastCompletionTime = currentLevel.CompletionTime;
		
        Explanation explanation;
        if (lastCollectedStars < currentCollectedStars)
        {
            explanation = lastCompletionTime == -1 || lastCompletionTime > currentCompletionTime ? 
                Explanation.BetterTimeMoreStars : Explanation.WorseTimeMoreStars;
        }
        else if (lastCollectedStars == currentCollectedStars)
        {
            explanation = lastCompletionTime == -1 || lastCompletionTime > currentCompletionTime ? 
                Explanation.BetterTimeEqualStars : Explanation.WorseTimeEqualStars;
        }
        else
        {
            explanation = lastCompletionTime == -1 || lastCompletionTime > currentCompletionTime ? 
                Explanation.BetterTimeLessStars : Explanation.WorseTimeLessStars;
        }
		
        panel.OpenLevelCompletePanel(explanation, 
            lastCollectedStars,
            currentCollectedStars,
            TimerUtils.GetFormatedTime(lastCompletionTime),
            TimerUtils.GetFormatedTime(currentCompletionTime));
        return explanation;
    }
}
