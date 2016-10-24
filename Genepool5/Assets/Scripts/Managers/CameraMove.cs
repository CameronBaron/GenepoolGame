using UnityEngine;
using InControl;

public class CameraMove : MonoBehaviour
{
	public InputDevice device { get; set; }
	public bool lockedIn = false;
	public Canvas readyCanvas;	
	public AudioClip selectSound;

	private bool pressed = true;

	void Start()
	{
		readyCanvas.enabled = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!lockedIn)
		{
			ControllerInput();
		}
	}

	void ControllerInput()
	{
		if (!pressed)
		{
			if (device.Action1.IsPressed)
			{
				lockedIn = true;
				readyCanvas.enabled = true;
				SoundManager.PlaySingle(0.3f, selectSound, false);
			}
		}
		else if (!device.Action1.IsPressed)
		{
			pressed = false;
		}

	}
}
