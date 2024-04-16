using UnityEngine;

public class InLevelState : State
{
    [Header("Players")]
    [SerializeField] private Character bigPlayerPrefab;
    [SerializeField] private Character smallPlayerPrefab;

    private bool wasTutorialShown = false;
    
    public override void OnEnter()
    {
        base.OnEnter();
        if (GameManager.Instance.Data.CurrentLvl == 0 && !wasTutorialShown) 
        {
            wasTutorialShown = true;
            GoToTutorialState();
            return;
        }
        
        Time.timeScale = 1;
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Gameplay);

        // Fing LevelManager class that contains info about level for eq. pos of players, where should be placed.
        KeyBindingsManager.instance.LoadKeyBindings();
    }
            
    private void GoToTutorialState()
    {
        GameManager.Instance.Data.Status = GameData.GameStatus.Tutorial;
    }
}
