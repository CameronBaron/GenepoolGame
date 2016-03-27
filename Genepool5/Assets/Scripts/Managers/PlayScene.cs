using UnityEngine;
using System.Collections;
using InControl;

public class PlayScene : MonoBehaviour
{
	public Canvas pauseMenu;
	public GameObject gameTimer;

	GameManager GM;
	private bool paused;
	private bool zeroTime { get { return gameTimer.GetComponent<GameTime>().zeroTime; } }

	// Use this for initialization
	void Awake ()
	{
        GM = GameManager.Instance;
		SoundManager.SceneMusic(GameState.PLAY);
		pauseMenu.enabled = false;
		//GM.OnStateChange += HandleOnStateChangeWIN;
	}	

	// Update is called once per frame
	void Update()
	{
		if (InputManager.ActiveDevice.Command.WasPressed && !paused)
		{
			//Load pause screen overlay
			pauseMenu.enabled = true;
			//Pause game time
			Time.timeScale = 0;
			paused = true;
		}
		else if (InputManager.ActiveDevice.Command.WasPressed && paused)
		{
			//Get rid of pause screen
			//Unpause game time
			GM.OnStateChange += HandleOnStateChangeMENU;
			GM.SetGameState(GameState.MENU);
		}
		else if (paused && InputManager.ActiveDevice.Action2.IsPressed)
		{
			//Unpause game
			pauseMenu.enabled = false;
			Time.timeScale = 1;
			paused = false;
		}

		//If timer gets to 0
		if (zeroTime)
		//Go to end game screen
		{
			GM.OnStateChange += HandleOnStateChangeWIN;
			GM.SetGameState(GameState.WIN);
		}
	}

	public void HandleOnStateChangeWIN()
	{
		Invoke("LoadWINScene", 0f);
		GM.OnStateChange -= HandleOnStateChangeWIN;
	}

	public void HandleOnStateChangeMENU()
	{
		Invoke("LoadMenu", 0f);
		Time.timeScale = 1;
		GM.OnStateChange -= HandleOnStateChangeMENU;
	}

	public void LoadWINScene()
	{
		Application.LoadLevel((int)GameState.WIN);
	}

	public void LoadMenu()
	{
		Application.LoadLevel((int)GameState.MENU);
	}	

}
