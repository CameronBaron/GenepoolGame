/// Created by Cameron Baron 19/09/2015

using UnityEngine;
using InControl;

public enum WeaponType
{
	FlameThrower, // 0
	RocketLauncher, // 1
	AK47, // 2
	Shotgun, // 3
	Knife // 4
}

[System.Serializable]
public class Weapons
{
	public GameObject flameThrower;
	public GameObject rocketLauncher;
	public GameObject ak47;
	public GameObject shotgun;
	public GameObject knife;
};

public class Stats
{
	public int score = 0;
	public int deaths = 0;
	public int bulletsFired = 0;
	public int kills = 0;
	public float accuracy = 0;
};

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	// Public Variables
	public InputDevice Device { get; set; }
	public int playerID;
	public float speed = 30;
	public float meleeDamage = 10;
	public float maxSpeed = 15;
	public GameObject indicator;
	public Color colour;
	public GameObject weaponSpawn;
	public Weapons weapons;

	[HideInInspector]	public int scoreValue = 1;
	[HideInInspector]	public GameObject spawnPos;
	[HideInInspector]	public Health health;
	[HideInInspector]	public bool disableControls = false;
	[HideInInspector]	public Stats stats;
	[HideInInspector]	WeaponType currentWeapon;

	// Private Variables
	private Vector3 moveVector = Vector3.zero;
	private Rigidbody body;
	private Animator animator;

	void Awake()
	{
		gameObject.tag = "Player";
		body = GetComponent<Rigidbody>();
		health = GetComponent<Health>();
		animator = GetComponentInChildren<Animator>();

		#region Weapons

		weapons.ak47.gameObject.SetActive(false);
		weapons.flameThrower.gameObject.SetActive(false);
		weapons.rocketLauncher.gameObject.SetActive(false);
		weapons.shotgun.gameObject.SetActive(false);

		currentWeapon = WeaponType.AK47;
		ChangeWeapon();

		#endregion
	}

	void Update()
	{
		if (Device.DPadLeft.IsPressed)
		{
			currentWeapon = WeaponType.FlameThrower;
			ChangeWeapon();
		}
		else if (Device.DPadUp.IsPressed)
		{
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

			currentWeapon = WeaponType.AK47;
			ChangeWeapon();
		}
		else if (Device.DPadRight.IsPressed)
		{
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

			currentWeapon = WeaponType.RocketLauncher;
			ChangeWeapon();
		}
		else if (Device.DPadDown.IsPressed)
		{
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

			currentWeapon = WeaponType.Shotgun;
			ChangeWeapon();
		}
	}

	void FixedUpdate()
	{
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

	public void SetupPlayer(Color a_color, int a_id, Transform a_spawn)
	{
		colour = a_color;
		indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", a_color);
		// Change player color material colour.
		//
		Device = GameManager.Instance.devices[a_id];
		playerID = a_id + 1;
		GameManager.Instance.players.Add(gameObject);
		spawnPos = a_spawn.gameObject;
		DontDestroyOnLoad(gameObject);
	}

	void ChangeWeapon()
	{
		weapons.flameThrower.gameObject.SetActive(false);
		weapons.ak47.gameObject.SetActive(false);
		weapons.rocketLauncher.gameObject.SetActive(false);
		weapons.shotgun.gameObject.SetActive(false);

		switch (currentWeapon)
		{
			case WeaponType.FlameThrower:
				weapons.flameThrower.gameObject.SetActive(true);
				break;
			case WeaponType.AK47:
				weapons.ak47.gameObject.SetActive(true);
				break;
			case WeaponType.Shotgun:
				weapons.shotgun.gameObject.SetActive(true);
				break;
			case WeaponType.RocketLauncher:
				weapons.rocketLauncher.gameObject.SetActive(true);
				break;
		}
	}
}