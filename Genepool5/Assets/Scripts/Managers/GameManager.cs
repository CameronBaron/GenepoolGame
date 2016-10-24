using UnityEngine;
using System.Collections.Generic;
using InControl;


public enum GameState
{
	//INTRO,
	MENU,
	CHARACTERS,
	//PAUSE,
	PLAY,
	WIN,
	//EXIT,
	ARENA1,
	ARENA2,
	ARENA3,
}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
	const int maxPlayers = 4;
	protected GameManager() { }
	private static GameManager instance = null;
	public event OnStateChangeHandler OnStateChange;
	public GameState gameState { get; private set; }

	public List<int> chosenType = new List<int>();
	public List<GameObject> players = new List<GameObject>();
	public List<bool> isPlaying = new List<bool>();
	public List<InputDevice> devices = new List<InputDevice>();

	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameManager();
			}
			return instance;
		}
	}

	void Start()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			DestroyImmediate(gameObject);
		}
	}

	public void SetGameState(GameState state)
	{
		gameState = state;
		OnStateChange();		
	}

	public void OnApplicationQuit()
	{
		instance = null;
	}
}