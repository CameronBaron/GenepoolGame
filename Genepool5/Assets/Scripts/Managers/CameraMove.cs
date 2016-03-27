using UnityEngine;
using InControl;

public class CameraMove : MonoBehaviour
{
	public InputDevice device { get; set; }

	private float[] cameraXPos = {0, 10, 20, 30};
	public int targetChar;
	public int playerID;
	public bool lockedIn = false;
	public Canvas readyCanvas;

	public AudioClip tickSound;
	public AudioClip selectSound;

	private int currentChar = 0;
	private float lerpTime = 0;
	private bool pressed = true;
	private bool moving = false;

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
			// Clamp targetChar as to not go out of bounds
			targetChar = Mathf.Clamp(targetChar, 0, cameraXPos.Length - 1);

			if (currentChar != targetChar)
				ChangeFocus();
		}
	}

	void ChangeFocus()
	{
		Vector3 targetPos = new Vector3(cameraXPos[targetChar], transform.position.y, transform.position.z);

		transform.position = Vector3.Lerp(transform.position, targetPos, lerpTime);
		lerpTime += Time.deltaTime;
		moving = true;

		if (lerpTime > 1)
		{
			transform.position = targetPos;
			lerpTime = 0;
			currentChar = targetChar;
			moving = false;
        }
	}

	void ControllerInput()
	{
		targetChar = Mathf.Clamp(targetChar, 0, cameraXPos.Length - 1);

		if (!pressed && !moving)
		{
			if (device.LeftStickX < 0 || device.DPadX < 0)
			{
				targetChar -= 1;
				pressed = true;
				SoundManager.PlaySingle(0.3f, tickSound, false);
			}
			else if (device.LeftStickX > 0 || device.DPadX > 0)
			{
				targetChar += 1;
				pressed = true;
				SoundManager.PlaySingle(0.3f, tickSound, false);
			}

			if (device.Action1.IsPressed)
			{
				lockedIn = true;
				readyCanvas.enabled = true;
				GameManager.Instance.chosenType.Add(playerID, (BasePlayer.PLAYERTYPE)targetChar);
				SoundManager.PlaySingle(0.3f, selectSound, false);
			}
		}
		else if (device.LeftStickX == 0 && device.DPadX == 0 && !device.Action1.IsPressed)
		{
			pressed = false;
		}

	}
}
