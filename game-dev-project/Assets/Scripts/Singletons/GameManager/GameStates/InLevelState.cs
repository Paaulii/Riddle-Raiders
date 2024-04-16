using UnityEngine;

public class InLevelState : State
{
    [SerializeField] StarCollectDetector starCollectDetector;
    private bool wasTutorialShown = false;
    private GameUIPanel panel;
    public override void OnEnter()
    {
        base.OnEnter();

        if (OpenTutorialIfFirstLevel())
        {
            return;
        }

        panel = PanelManager.instance.GetPanel<GameUIPanel>();
        Time.timeScale = 1;
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Gameplay);
        KeyBindingsManager.instance.LoadKeyBindings();
        
        PlayersManager playersManager = PlayersManager.instance;
        playersManager.ForceStartPlayersMovement();
        playersManager.onPlayerHit += HandlePlayerHit;
        playersManager.onPlayersDeath += HandlePlayerDeath;
        
        starCollectDetector.FindAllStartAtLevel();
        starCollectDetector.onStarCollected += HandleStarCollected;
    }

    private void HandleStarCollected(Vector2 starPosition)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Collect, starPosition);
        panel.IncreaseStarAmount();
    }

    public override void OnExit()
    {
        base.OnExit();
        PlayersManager playersManager = PlayersManager.instance;
        playersManager.onPlayerHit -= HandlePlayerHit;
        playersManager.onPlayersDeath -= HandlePlayerDeath;
        
        starCollectDetector.onStarCollected -= HandleStarCollected;
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
        if (GameManager.Instance.Data.CurrentLvl == 0 && !wasTutorialShown) 
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
