using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance => instance;
	public GameData Data => data;
	
	private readonly GameData data = new();
	private static GameManager instance;

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
	
	[Header("Sound Managers")]
	
	[Header("Data Manager")]
	[SerializeField] private DataManager dataManager;
	[SerializeField] private Timer timer;
	PlayerGameProgress playerGameProgress;

	[Header("Effects")]
	[SerializeField] private GameObject fireworkEffect;
	[SerializeField] private GameObject fireworkShootEffect;
	[SerializeField] private GameObject smokeEffect;

	[SerializeField] private GameSettingsSO GameSettings => _GameSettings;
	private GameSettingsSO _GameSettings;

	private State currentState;
	
	private void Awake()
	{
		instance = this;
	}
	
	private void Start()
	{
		ChangeCurrentState(GameData.GameStatus.MainMenu);
		data.onStateChange += ChangeCurrentState;
		/*BindToEvents();
		playerGameProgress = dataManager.LoadData();
		uiController.SetLevelNumber(dataManager.CurrentLvl);
		uiController.onCloseTutorial += HandleCloseTutorialPanel;
		
		if (dataManager.CurrentLvl == 1)
		{
			HandleOpenTutorialPanel();
		}
		
		LoadKeyBindings(GameSettings.player1KeyBindingsConfKey, smallPlayer, GameSettings.player1ActionMap);
		LoadKeyBindings(GameSettings.player2KeyBindingsConfKey, bigPlayer, GameSettings.player2ActionMap);
*/
		DontDestroyOnLoad(this);
	}

	private void ChangeCurrentState(GameData.GameStatus status) 
	{
		currentState?.OnExit();
		currentState = gameStates.FirstOrDefault(x => x.status == status)?.state;
		currentState?.OnEnter();
	}
	
	private void Update()
	{
		currentState?.Update();
	}
	
	private void HandleCloseTutorialPanel()
	{
		timer.ResetTimer();
		timer.IsCounting = true;
		bigPlayer.Movement.ForceStartPlayer();
		smallPlayer.Movement.ForceStartPlayer();
		uiController.onCloseTutorial -= HandleCloseTutorialPanel;
	}

	private void HandleOpenTutorialPanel()
	{
		timer.IsCounting = false;
		uiController.OpenTutorialPanel();
		bigPlayer.Movement.ForceStopPlayer();
		smallPlayer.Movement.ForceStopPlayer();
	}

	private void OnDestroy()
	{
		UnbindFromEvents();
	}

	private void BindToEvents()
	{
		bigPlayer.onPlayersDeath += HandleGameOverState;
		bigPlayer.onPlayerHit += HandlePlayerHit;
		
		smallPlayer.onPlayersDeath += HandleGameOverState;
		smallPlayer.onPlayerHit += HandlePlayerHit;

		starCollectDetector.onStarCollected += HandleStarCollecting;

		endDoor.onEnterEndDoor += HandleEndLevel;

		uiController.onBackToMenu += HandleBackToMenu;
		uiController.onNextLevelButtonClicked += HandleNextLevelButtonClicked;
		uiController.onResetLevel += HandleResetLevel;

		uiController.onResumeLevel += HandleResume;

		smallPlayer.Movement.onEscPressed += HandlePauseGame;

		timer.onTimeTick += HandleTimeTick;
	}

	private void HandleTimeTick(float currentTime)
	{
		uiController.UpdateTime(GetFormatedTime(currentTime));
	}

	private string GetFormatedTime(float time)
	{
		if (time == -1) 
		{
			return "--:--";
		}
		
		int minutes = Mathf.FloorToInt(time / 60);
		int seconds = Mathf.FloorToInt(time % 60);
		
		StringBuilder builder = new StringBuilder();
		builder.Append(minutes < 10 ? "0" : "");
		builder.Append(minutes);
		builder.Append(":");
		builder.Append(seconds < 10 ? "0" : "");
		builder.Append(seconds);
		return builder.ToString();
	}
	
	private void HandleResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void HandleBackToMenu()
	{
		SceneManager.LoadScene("Menu");
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
		dataManager.CurrentLvl++;
		int levelIndex = dataManager.CurrentLvl - 1;
		
		if (levelIndex >= 0) {
			SceneManager.LoadScene(playerGameProgress.LevelsData[levelIndex].PathToScene);
		}
	}

	private void HandleStarCollecting()
	{
		SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Collect, smallPlayer.transform.position);
		uiController.IncreaseStarAmount();
	}

	private void UnbindFromEvents()
	{
		/*data.onStateChange -= ChangeCurrentState;
		bigPlayer.onPlayersDeath -= HandleGameOverState;
		bigPlayer.onPlayerHit -= HandlePlayerHit;
		
		smallPlayer.onPlayersDeath -= HandleGameOverState;
		smallPlayer.onPlayerHit -= HandlePlayerHit;
		
		starCollectDetector.onStarCollected -= HandleStarCollecting;
		
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
		dataManager.SaveData(explanation, starCollectDetector.StarsAmount, dataManager.CurrentLvl, timer.CurrentTime);


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

		if (dataManager.CurrentLvl == playerGameProgress.LevelsData.Count)
		{
			uiController.HandleGameComplete();
		}
	}

	private Explanation OpenLevelCompletePanel()
	{
		int levelIndex = dataManager.CurrentLvl - 1;
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
											GetFormatedTime(lastCompletionTime),
											GetFormatedTime(currentCompletionTime));
		return explanation;
	}

	private void HandlePlayerHit(Character character)
	{
		SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Hit, character.transform.position);
		uiController.DecreasePlayersHealth(character.Type);
	}
	
	private void HandleGameOverState()
	{
		timer.IsCounting = false;
		uiController.SetActiveGameOverPanel(true);
		bigPlayer.Movement.ForceStopPlayer();
		smallPlayer.Movement.ForceStopPlayer();
	}

	private void HandlePauseGame()
	{
		timer.IsCounting = false;
		uiController.SetActivePausePanel(true);
		bigPlayer.Movement.ForceStopPlayer();
		smallPlayer.Movement.ForceStopPlayer();
	}

	private void LoadKeyBindings(string confKey, Character character, string actionMap)
	{
		string rebinds = PlayerPrefs.GetString(confKey, string.Empty);

		if (!string.IsNullOrEmpty(rebinds))
		{
			character.Movement.PlayerInput.actions.FindActionMap(actionMap).LoadBindingOverridesFromJson(rebinds);
		}
	}
}
