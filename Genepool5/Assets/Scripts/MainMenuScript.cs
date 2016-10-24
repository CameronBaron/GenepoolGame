using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioClip uiSound;
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
	public GameObject noButton;
	[HideInInspector]	public GameManager GM;

	// Use this for initialization
	void Start ()
	{
		GM = GameManager.Instance;
		//GM.chosenType.Clear();
		GM.players.Clear();
		GM.isPlaying.Clear();
		GM.devices.Clear();
        SoundManager.SceneMusic(GameState.MENU);
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;	

		foreach (GameObject p in GM.players)
		{
			DestroyImmediate(p);
		}
	}

	public void ExitPress ()
	{
        SoundManager.PlaySingle(0.5f, uiSound, false);
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
		EventSystem.current.SetSelectedGameObject(noButton, null);
    }

    public void NoPress()
	{
        SoundManager.PlaySingle(0.5f, uiSound, false);
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
		EventSystem.current.SetSelectedGameObject(startText.gameObject, null);
	}

    public void StartLevel()
	{
        SoundManager.PlaySingle(0.5f, uiSound, false);
		GM.OnStateChange += HandleOnStateChangePICK;
		GM.SetGameState(GameState.CHARACTERS);
	}

	public void HandleOnStateChangePICK()
	{
		Invoke("LoadNext", 1f);
		GM.OnStateChange -= HandleOnStateChangePICK;
	}

	public void LoadNext()
	{
		SceneManager.LoadScene((int)GameState.CHARACTERS, LoadSceneMode.Single);
	}

    public void ExitGame()
	{
        SoundManager.PlaySingle(0.5f, uiSound, false);
        Application.Quit();
    }

    public void OnMove()
    {
        SoundManager.PlaySingle(0.5f, uiSound, false);
    }
}
