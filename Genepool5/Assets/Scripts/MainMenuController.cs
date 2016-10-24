using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	string[] buttons = new string[4]{ "Play", "Exit" , "Yes", "No" };
	string selected;

	// Use this for initialization
	void Start ()
	{
		selected = buttons[0];
    }
	
	// Update is called once per frame
	void Update ()
	{
		MainMenuControl();
	}
	

	void MainMenuControl()
	{
		if (Input.GetAxis("Left Stick Vertical") > 0.2f || Input.GetAxis("DPAD Vertical") < -0.2f)
		{
			//Down
			selected = buttons[1];
		}

		if (Input.GetAxis("Left Stick Vertical") < -0.2f || Input.GetAxis("DPAD Vertical") > 0.2f)
		{
			//Up
			selected = buttons[0];
		}
				
		GUI.SetNextControlName(selected);
	}

	void QuitMenuControl()
	{
		if (Input.GetAxis("Left Stick Horizontal") > 0.2f || Input.GetAxis("DPAD Horizontal") < -0.2f)
		{
			//Left
			selected = buttons[2];
		}

		if (Input.GetAxis("Left Stick Horizontal") < -0.2f || Input.GetAxis("DPAD Horizontal") > 0.2f)
		{
			//Right
			selected = buttons[3];
		}

		if (selected != buttons[2] || selected != buttons[3])
		{
			GUI.SetNextControlName(buttons[3]);
		}
		else
		{
			GUI.SetNextControlName(selected);
		}
	}
}
