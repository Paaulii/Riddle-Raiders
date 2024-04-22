using UnityEngine;

public class InLevelState : State
{
    [SerializeField] StarCollectDetector starCollectDetector;
    private LevelController levelController;
    private bool wasTutorialShown;
    private bool wasInPauseState;
    private GameUIPanel panel;
    
    public override void OnEnter()
    {
        base.OnEnter();
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Gameplay);

        if (OpenTutorialIfFirstLevel())
        {
            return;
        }

        panel = PanelManager.instance.ShowPanel<GameUIPanel>();
        KeyBindingsManager.instance.LoadKeyBindings();
        SetupTimer();
        GetLevelData();
        SetupPlayerManager();
        SetupStarDetector();
        wasInPauseState = false;
    }
    
    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (Input.GetKeyDown(GameManager.Instance.GameSettings.PauseButton))
        {
            wasInPauseState = true;
            GameManager.Instance.Data.Status = GameData.GameStatus.InPause;
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
        PlayersManager playersManager = PlayersManager.instance;
        playersManager.onPlayerHit -= HandlePlayerHit;
        playersManager.onPlayersDeath -= HandlePlayerDeath;

        Timer.instance.onTimeTick -= HandleTimeTick;
        starCollectDetector.onStarCollected -= HandleStarCollected;
        
        if (levelController)
        {
            levelController.onEnterDoor -= HandleEndLevel;
        }
    }
    
    private void SetupTimer()
    {
        Time.timeScale = 1;
        
        if (!wasInPauseState)
        {
            Timer.instance.ResetTimer();
        }
        
        Timer.instance.StartTimer(); 
        Timer.instance.onTimeTick += HandleTimeTick;
    }

    private void HandleTimeTick(float time)
    {
        panel.UpdateTime(TimerUtils.GetFormatedTime(time));
    }

    private void SetupStarDetector()
    {
        starCollectDetector.FindAllStarsAtLevel();
        starCollectDetector.onStarCollected += HandleStarCollected;
    }
    
    private void HandleStarCollected(Vector2 starPosition)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Collect, starPosition);
        panel.IncreaseStarAmount();
    }
    
    private void SetupPlayerManager()
    {
        PlayersManager playersManager = PlayersManager.instance;
        playersManager.ForceStartPlayersMovement();
        playersManager.onPlayerHit += HandlePlayerHit;
        playersManager.onPlayersDeath += HandlePlayerDeath;
    }
    
    private void GetLevelData()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.onEnterDoor += HandleEndLevel;
    }

    private void HandleEndLevel()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.SummaryScreen;
    }
    
    private void HandlePlayerDeath()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.LoseScreen;
    }

    private void HandlePlayerHit(Character character)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Hit, character.transform.position);
        panel.DecreasePlayersHealth(character.Type);
    }

    private bool OpenTutorialIfFirstLevel()
    {
        if (GameManager.Instance.Data.CurrentLevel.LevelNumber == 0 && !wasTutorialShown) 
        {
            wasTutorialShown = true;
            OpenTutorial();
            return true;
        }

        return false;
    }

    private void OpenTutorial()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.Tutorial;
    }
}
