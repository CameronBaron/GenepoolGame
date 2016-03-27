/// Created by Cameron Baron 19/09/2015

using UnityEngine;
using InControl;

public class Player : MonoBehaviour
{
	// Public Variables
	public InputDevice Device { get; set; }
	public int playerID;
	public float speed = 30;
	public float meleeDamage = 10;
	public float maxSpeed = 15;
	public GameObject indicator;
	public Color colour;

	[HideInInspector]	public int scoreValue = 1;
	[HideInInspector]	public int score;
	[HideInInspector]	public GameObject spawnPos;
	[HideInInspector]	public Health health;
	[HideInInspector]	public bool disableControls = false;

	[HideInInspector]	public int deaths = 0;
	[HideInInspector]	public int bulletsFired = 0;
	[HideInInspector]	public int kills = 0;
	[HideInInspector]	public float accuracy = 0;

	// Private Variables
	private Vector3 moveVector;
	private Rigidbody body;

	// Use this for initialization.
	void Awake()
	{
		score = 0;
		gameObject.tag = "Player";
		moveVector = Vector3.zero;
		body = GetComponent<Rigidbody>();
		health = GetComponent<Health>();
	}

	// Stuff to get called before the main Update function.
	void FixedUpdate()
	{
		if (body == null)
			Debug.Log("body = null");
		MovementManager();
	}

	/// <summary>
	/// Checks for controller input and applies the forces to the player.
	/// </summary>
	void MovementManager()
	{
		#region XBox Controller
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~            XBox Controllers           ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		if (!disableControls)
		{
			// Moving the players position.
			if (Device.LeftStick.X != 0f || Device.LeftStickY != 0f)
			{
				// Get the direction of the left thumbstick
				moveVector = new Vector3(Device.LeftStick.X, 0, Device.LeftStickY);

				// Check if player is below max speed then add force
				if (body.velocity.magnitude < maxSpeed)
				{
					body.AddForce(moveVector * speed);
				}
			}

			// For player rotation. Check if stick has been moved.
			if (Device.RightStickX != 0f || Device.RightStickY != 0f)
			{
				// Simply makes a position near the gameObject based on controller joystick axes!
				Vector3 lookAtPosition = transform.position + new Vector3(Device.RightStickX, 0.0f, Device.RightStickY);
				Vector3 rotation = transform.rotation.eulerAngles;  // Save the current rotation
				transform.LookAt(lookAtPosition);                   // Look at that position we just made. This will change the local rotation. Hence why we saved it
				transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotation), transform.rotation, 0.2f); // Lerp between your original and new rotation.
			}
		}
		#endregion
	}
}