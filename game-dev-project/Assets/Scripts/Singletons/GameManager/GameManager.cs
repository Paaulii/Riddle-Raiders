using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance => instance;
	public GameData Data => data;
	public GameSettingsSO GameSettings => _gameSettings;
	private static GameManager instance;
	private readonly GameData data = new();
	
	[SerializeField] private GameStatus[] gameStates;
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
		currentState?.OnUpdate();
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
}
