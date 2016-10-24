using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSceenManager : MonoBehaviour
{
	GameManager GM = GameManager.Instance;
	
	// Update is called once per frame
	void Update ()
	{
		// If timer < 0 and start is pressed
		if (Input.GetButton("Start"))
		{
			GM.OnStateChange += HandleOnStateChangeMenu;
			GM.SetGameState(GameState.MENU);
		}
	}

	public void HandleOnStateChangeMenu()
	{
		Invoke("LoadMenu", 1f);
		GM.OnStateChange -= HandleOnStateChangeMenu;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene((int)GameState.MENU);
	}
}
