using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
	public HealthBar healthbar;

	private int getColour = 0;
	private PlayerController pc = null;

	public Text score
	{
		get { return GetComponent<Text>(); }
	}

	void Start()
	{
		score.text = "0";
	}

	void Update ()
	{
		if (healthbar == null || healthbar.playerRef == null)
		{
			gameObject.SetActive(false);
		}
		else if (getColour == 0 && healthbar.playerRef != null)
		{
			pc = healthbar.playerRef.GetComponent<PlayerController>();
			score.color = pc.colour;
			getColour++;
			score.text = pc.stats.score.ToString();
		}
		
	}
}
