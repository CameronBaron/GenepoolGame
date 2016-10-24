using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
	private bool paused = false;
	private GameManager gm;


	// Use this for initialization
	void Start ()
	{
		gm = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (InputManager.ActiveDevice.Command.WasPressed && !paused)
		{
			//Load pause screen overlay
			//Pause game time
			Time.timeScale = 0;
			paused = true;
		}
		else if (InputManager.ActiveDevice.Command.WasPressed && paused)
		{
			//Get rid of pause screen
			paused = false;
			//Unpause game time
			HandleOnStateChangeMENU();
        }
		else if (paused && InputManager.ActiveDevice.Action2.IsPressed)
		{
			//Unpause game
			Time.timeScale = 1;
			paused = false;
		}

	}

	void HandleOnStateChangeMENU()
	{
		Invoke("LoadMenu", 1f);
		gm.SetGameState(GameState.MENU);
		gm.OnStateChange -= HandleOnStateChangeMENU;
	}

	void LoadMenu()
	{
		SceneManager.LoadScene((int)GameState.MENU);
	}
}
