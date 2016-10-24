using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
	[HideInInspector]
	public string gameTime
	{
		get { return GetComponent<Text>().text; }
		private set { GetComponent<Text>().text = value; }
	}
	public bool zeroTime = false;
	public int timeInMins = 5;
	
	private float sec;
	private int min;

	// Use this for initialization
	void Start ()
	{
		min = timeInMins;
		sec = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sec -= Time.deltaTime;

		if (min <= 0 && sec < 1)
		{
			zeroTime = true;
		}

		UpdateTimer();
	}

	void UpdateTimer()
	{
		if (sec < 0)
		{
			sec = 59;
			min -= 1;
		}

		gameTime = min.ToString() + ":" + sec.ToString("00");
	}
}
