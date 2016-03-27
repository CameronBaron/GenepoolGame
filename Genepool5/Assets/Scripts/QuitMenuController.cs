using UnityEngine;
using System.Collections;

public class QuitMenuController : MonoBehaviour
{
	public float repeatDelay = 0.5f;
	private float repeatTimer = 0;

	string[] buttons = new string[4] { "Play", "Exit", "Yes", "No" };
	string selected;

	// Use this for initialization
	void Start ()
	{
		selected = buttons[3];
	}
	
	// Update is called once per frame
	void Update ()
	{

    }


}
