using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetScores : MonoBehaviour
{
	public Text kills;
	public Text deaths;

	public void SetValues(GameObject playerRef)
	{
		Player player = playerRef.GetComponent<Player>();
		kills.text = player.score.ToString();
		kills.color = player.colour;

		deaths.text = player.deaths.ToString();
		deaths.color = player.colour;
	}
}
