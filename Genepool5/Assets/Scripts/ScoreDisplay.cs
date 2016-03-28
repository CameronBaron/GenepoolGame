using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour
{
	public Canvas scoreboard;
	public Text winnerText;

	public List<GameObject> ranking = new List<GameObject>();

	GameManager GM = GameManager.Instance;

	// Use this for initialization
	void Start()
	{
		ranking.Clear();
		GM.players.Sort((p1, p2) => p2.GetComponent<Player>().score.CompareTo(p1.GetComponent<Player>().score));
		WinnerText();
		DisplayScores();
    }

	public void WinnerText()
	{
		//if first in list has a colour of blue
		//winnerText.text = "BLUE WINS"
		//winnerText.tex.colour = Color.Blue
		winnerText.color = GM.players[0].GetComponent<Player>().colour;

		if (winnerText.color == Color.blue)
		{
			winnerText.text = "BLUE WINS!";
		}
		else if (winnerText.color == Color.red)
		{
			winnerText.text = "RED WINS!";
		}
		else if (winnerText.color == Color.green)
		{
			winnerText.text = "GREEN WINS!";
		}
		else if (winnerText.color == Color.yellow)
		{
			winnerText.text = "YELLOW WINS!";
		}
	}

	public void DisplayScores()
	{
		for (int i = 0; i < GM.players.Count; i++)
		{
			ranking[i].GetComponent<SetScores>().SetValues(GM.players[i]);
		}

		for (int i = GM.players.Count; i < ranking.Count; i++)
		{
			ranking[i].SetActive(false);
		}
	}
}
