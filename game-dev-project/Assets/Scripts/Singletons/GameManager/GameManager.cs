using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance => instance;
	public GameData Data => data;
	
	private readonly GameData data = new();
	private static GameManager instance;

	public GameSettingsSO GameSettings => _gameSettings;
	
	[Header("Game states")] 
	[SerializeField] private GameStatus[] gameStates;
	[Header("Players")]
	[SerializeField] private Character bigPlayer;
	[SerializeField] private Character smallPlayer;

	[Header("Items")] 
	[SerializeField] private StarCollectDetector starCollectDetector;
	
	[Header("UI objects")] 
	[SerializeField] private UIController uiController;

	[Header("End level")]
	[SerializeField] private EnterEndDoor endDoor;
	
	[FormerlySerializedAs("dataManager")]
	[Header("Sound Managers")]
	
	[Header("Data Manager")]
	[SerializeField] private SaveSystemManager saveSystemManager;
	[SerializeField] private Timer timer;
	PlayerGameProgress playerGameProgress;

	[Header("Effects")]
	[SerializeField] private GameObject fireworkEffect;
	[SerializeField] private GameObject fireworkShootEffect;
	[SerializeField] private GameObject smokeEffect;

	[SerializeField] private GameSettingsSO _gameSettings;

	private State currentState;
	
	private void Awake()
	{
		instance = this;
	}
	
	private void Start()
	{
		ChangeCurrentState(GameData.GameStatus.MainMenu);
		data.onStateChange += ChangeCurrentState;
		DontDestroyOnLoad(this);
	}
	
	private void Update()
	{
		currentState?.Update();
	}
		
	private void OnDestroy()
	{
		data.onStateChange -= ChangeCurrentState;
	}
	
	private void ChangeCurrentState(GameData.GameStatus status) 
	{
		currentState?.OnExit();
		currentState = gameStates.FirstOrDefault(x => x.status == status)?.state;
		currentState?.OnEnter();
	}


	private void BindToEvents()
	{
		endDoor.onEnterEndDoor += HandleEndLevel;

		uiController.onNextLevelButtonClicked += HandleNextLevelButtonClicked;

		uiController.onResumeLevel += HandleResume;

		smallPlayer.Movement.onEscPressed += HandlePauseGame;
		
	}

	
	private void HandleResume()
	{
		timer.IsCounting = true;
		uiController.SetActivePausePanel(false);
		bigPlayer.Movement.ForceStartPlayer();
		smallPlayer.Movement.ForceStartPlayer();
	}

	private void HandleNextLevelButtonClicked()
	{
		if (++data.CurrentLvl >= 0) {
			SceneManager.LoadScene(playerGameProgress.LevelsData[data.CurrentLvl].PathToScene);
		}
	}

	private void UnbindFromEvents()
	{
		/*
		s
		
		endDoor.onEnterEndDoor -= HandleEndLevel;
		uiController.onResumeLevel -= HandleResume;
		uiController.onBackToMenu -= HandleBackToMenu;
		uiController.onNextLevelButtonClicked -= HandleNextLevelButtonClicked;
		smallPlayer.Movement.onEscPressed -= HandlePauseGame;
		
		timer.onTimeTick += HandleTimeTick;*/
	}

	private void HandleEndLevel()
	{
		timer.IsCounting = false;
		Explanation explanation = OpenLevelCompletePanel();
		saveSystemManager.SaveData(explanation, starCollectDetector.StarsAmount, data.CurrentLvl, timer.CurrentTime);


		if (explanation == Explanation.WorseTimeEqualStars || explanation == Explanation.BetterTimeLessStars || explanation == Explanation.WorseTimeLessStars)
		{
			SoundManager.Instance.PlayUISound(SoundManager.UISoundType.GameOver);
			Instantiate(smokeEffect, endDoor.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
		}
		else
		{
			SoundManager.Instance.PlayUISound(SoundManager.UISoundType.EndLevel);
			Instantiate(fireworkEffect, endDoor.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
			Instantiate(fireworkShootEffect, endDoor.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
		}

		if (data.CurrentLvl == playerGameProgress.LevelsData.Count)
		{
			uiController.HandleGameComplete();
		}
	}

	private Explanation OpenLevelCompletePanel()
	{
		int levelIndex = data.CurrentLvl;
		LevelData currentLevel = playerGameProgress.LevelsData[levelIndex];
		int lastCollectedStars = currentLevel.StarsAmount;
		int currentCollectedStars = starCollectDetector.StarsAmount;
		float lastCompletionTime = currentLevel.CompletionTime;
		float currentCompletionTime = timer.CurrentTime;
		
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
		
		uiController.OpenLevelCompletePanel(explanation, 
											lastCollectedStars,
											currentCollectedStars,
											TimerUtils.GetFormatedTime(lastCompletionTime),
											TimerUtils.GetFormatedTime(currentCompletionTime));
		return explanation;
	}
	

	private void HandlePauseGame()
	{
		timer.IsCounting = false;
		uiController.SetActivePausePanel(true);
		bigPlayer.Movement.ForceStopPlayer();
		smallPlayer.Movement.ForceStopPlayer();
	}
}
