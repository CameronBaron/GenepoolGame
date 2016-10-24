using UnityEngine;
using System.Collections;

public class QuitMenuController : MonoBehaviour
{

	string[] buttons = new string[4] { "Play", "Exit", "Yes", "No" };
	string selected;

	void Start ()
	{
		selected = buttons[3];
	}
}
