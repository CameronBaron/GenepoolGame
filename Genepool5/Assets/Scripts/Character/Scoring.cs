using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
	public GameObject healthbar;
	private int getColour = 0;

	public Text score
	{
		get { return GetComponent<Text>(); }
	}

	void Update ()
	{
		if (healthbar == null || healthbar.GetComponent<HealthBar>().playerRef == null)
		{
			gameObject.SetActive(false);
		}
		else if (getColour == 0 && healthbar.GetComponent<HealthBar>().playerRef != null)
		{
			score.color = healthbar.GetComponent<HealthBar>().playerRef.GetComponent<Player>().indicator.GetComponent<Renderer>().material.GetColor("_EmissionColor");
			getColour++;
		}
		
		score.text = healthbar.GetComponent<HealthBar>().playerRef.GetComponent<Player>().score.ToString();
	}
}
