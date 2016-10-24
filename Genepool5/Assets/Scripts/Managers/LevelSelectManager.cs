using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
	GameManager GM = GameManager.Instance;
	
	// Update is called once per frame
	void Update ()
	{
		//If back button is pressed
		//Go back to menu
		if (InputManager.ActiveDevice.Action2.IsPressed)
		{
			GM.OnStateChange += HandleOnStateChangeMenu;
        }
	}

	public void Arena1Press()
	{
		GM.OnStateChange += HandleOnStateChangeArena1;
    }
	public void Arena2Press()
	{
		GM.OnStateChange += HandleOnStateChangeArena2;
	}
	public void Arena3Press()
	{
		GM.OnStateChange += HandleOnStateChangeArena3;
	}

	//Handle Change State to Menu
	public void HandleOnStateChangeMenu()
	{
		GM.OnStateChange -= HandleOnStateChangeMenu;
	}
	public void LoadMenu()
	{
		SceneManager.LoadScene((int)GameState.MENU);
	}

	//Handle Change State to Arena1
	public void HandleOnStateChangeArena1()
	{
		GM.OnStateChange -= HandleOnStateChangeArena1;
	}
	public void LoadArena1()
	{
		//Application.LoadLevel((int)GameState.ARENA1);
	}

	//Handle Change State to Arena2
	public void HandleOnStateChangeArena2()
	{
		GM.OnStateChange -= HandleOnStateChangeArena2;
	}
	public void LoadArena2()
	{
		//Application.LoadLevel((int)GameState.ARENA2);
	}

	//Handle Change State to Arena3
	public void HandleOnStateChangeArena3()
	{
		GM.OnStateChange -= HandleOnStateChangeArena3;
	}
	public void LoadArena3()
	{
		//Application.LoadLevel((int)GameState.ARENA3);
	}
}
