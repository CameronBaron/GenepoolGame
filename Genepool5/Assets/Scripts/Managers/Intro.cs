using UnityEngine;

public class Intro : MonoBehaviour
{
	GameManager GM;

	void Awake()
	{
		GM = GameManager.Instance;                      // Get GameManager Reference.
		GM.OnStateChange += HandleOnStateChange;

		Debug.Log("Current gameState when Awakes: " + GM.gameState);
	}

	void Start()
	{
		Debug.Log("Current gameState when Starts: " + GM.gameState);
	}

	public void HandleOnStateChange()
	{
		GM.SetGameState(GameState.MENU);
		Debug.Log("Handling state change to: " + GM.gameState);
		Invoke("LoadLevel", 3f);
		GM.OnStateChange -= HandleOnStateChange;
	}

	public void LoadLevel()
	{
		Application.LoadLevel("MainMenu");
	}
}
