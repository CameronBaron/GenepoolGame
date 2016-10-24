using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
	GameManager GM = GameManager.Instance;
	PlayerManager PM;
	private int i = 0;
	
	void Awake()
	{
		PM = FindObjectOfType<PlayerManager>();

		GM.chosenType.Clear();
		GM.isPlaying.Clear();
		GM.devices.Clear();

		GM.OnStateChange += HandleOnStateChangePLAY;
    }

	void Update()
	{
		// Save player info here to pass to game?				
		if (PM.goToGame && i < 1)
		{
			GM.SetGameState(GameState.PLAY);
			Invoke("LoadGame", 1f);
			GM.OnStateChange -= HandleOnStateChangePLAY;
			i++;
        }
	}

	public void HandleOnStateChangePLAY()
	{

	}

	public void LoadGame()
	{
		SceneManager.LoadScene((int)GameState.PLAY);
	}
}
