using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetScores : MonoBehaviour
{
	public Text kills;
	public Text deaths;

	public void SetValues(GameObject playerRef)
	{
		PlayerController player = playerRef.GetComponent<PlayerController>();
		kills.text = player.stats.score.ToString();
		kills.color = player.colour;

		deaths.text = player.stats.deaths.ToString();
		deaths.color = player.colour;
	}
}
