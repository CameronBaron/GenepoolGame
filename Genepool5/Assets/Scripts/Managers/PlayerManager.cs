using UnityEngine;
using System.Collections.Generic;
using InControl;

public class PlayerManager : MonoBehaviour
{
	const int maxPlayers = 4;

	List<Camera> playerCameras = new List<Camera>( maxPlayers );
	public Queue<int> playerID = new Queue<int>();
	public Queue<Rect> viewports = new Queue<Rect>();
	public GameObject cameraPrefab;
	public Vector3 startPos;
	public bool playersReady = false;
	public bool goToGame = false;
	public Canvas pressStart;

	GameManager GM;

	// Use this for initialization
	void Start ()
	{
		GM = GameManager.Instance;
		pressStart.enabled = false;
		InputManager.OnDeviceDetached += OnDeviceDetached;

		viewports.Enqueue(new Rect(0, 0, 0.25f, 1f));
		viewports.Enqueue(new Rect(0.25f, 0, 0.25f, 1f));
		viewports.Enqueue(new Rect(0.5f, 0, 0.25f, 1f));
		viewports.Enqueue(new Rect(0.75f, 0, 0.25f, 1f));

		playerID.Enqueue(1);
		playerID.Enqueue(2);
		playerID.Enqueue(3);
		playerID.Enqueue(4);
	}
	
	// Update is called once per frame
	void Update ()
	{
		var inputDevice = InputManager.ActiveDevice;

		if (JoinButtonWasPressedOnDevice(inputDevice))
		{
			if (ThereIsNoPlayerUsingDevice(inputDevice))
			{
				CreatePlayer(inputDevice);
			}
		}

		if (!playersReady)
		{			
			PlayersLockedIn();
		}
		else if(Input.GetButton("Start"))
		{
			goToGame = true;
		}
		else if (playersReady)
		{
			pressStart.enabled = true;
		}
	}

	public void SelectChar()
	{
		//else if (InputManager.GetButton("") && cameraP2.gameObject.activeSelf)
		//{
		//	//set char and stuff
		//	cameraP2.GetComponent<CameraMove>().lockedIn = true;
		//	GM.chosenType[1] = (BasePlayer.PLAYERTYPE)cameraP2.GetComponent<CameraMove>().targetChar;
		//	GM.isPlaying[1] = true;
		//}
		//else if (InputManager.GetButton("") && cameraP3.gameObject.activeSelf)
		//{
		//	//set char and stuff
		//	cameraP3.GetComponent<CameraMove>().lockedIn = true;
		//	GM.chosenType[2] = (BasePlayer.PLAYERTYPE)cameraP3.GetComponent<CameraMove>().targetChar;
		//	GM.isPlaying[2] = true;
		//}
		//else if (InputManager.GetButton("") && cameraP4.gameObject.activeSelf)
		//{
		//	//set char and stuff
		//	cameraP4.GetComponent<CameraMove>().lockedIn = true;
		//	GM.chosenType[3] = (BasePlayer.PLAYERTYPE)cameraP4.GetComponent<CameraMove>().targetChar;
		//	GM.isPlaying[3] = true;
	}

	public void PlayersLockedIn()
	{
		foreach (Camera c in playerCameras)
		{
			if (c.GetComponent<CameraMove>().lockedIn)
			{
				playersReady = true;
				continue;
			}
			else
			{
				playersReady = false;
				return;
			}
		}
	}

	//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

	bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
	{
		return inputDevice.Command.WasPressed;
	}

	Camera FindPlayerUsingDevice(InputDevice inputDevice)
	{
		var cameraCount = playerCameras.Count;
		for (int i = 0; i < cameraCount; i++)
		{
			var cam = playerCameras[i];
			if (cam.GetComponent<CameraMove>().device == inputDevice)
			{
				return cam;
			}
		}

		return null;
	}

	bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
	{
		return FindPlayerUsingDevice(inputDevice) == null;
	}

	void OnDeviceDetached(InputDevice inputDevice)
	{
		var cam = FindPlayerUsingDevice(inputDevice);
		if (cam != null)
		{
			RemovePlayer(cam);
		}
	}

	Camera CreatePlayer(InputDevice inputDevice)
	{
		if (playerCameras.Count < maxPlayers)
		{
			var gameObject = (GameObject)Instantiate(cameraPrefab, startPos, Quaternion.identity);
			var cam = gameObject.GetComponent<Camera>();
			cam.rect = viewports.Dequeue();
			cam.GetComponent<CameraMove>().device = inputDevice;
			GM.devices.Add(inputDevice);
			cam.GetComponent<CameraMove>().playerID = playerID.Dequeue();
			playerCameras.Add(cam);
			GM.isPlaying.Add(true);
			cam.GetComponent<CameraMove>().lockedIn = false;
            //GetComponentInChildren<Frames>().CreateNewFrame();

			return cam;
		}

		return null;
	}

	void RemovePlayer(Camera cam)
	{
		playerCameras.Remove(cam);
		playerID.Enqueue(cam.GetComponent<CameraMove>().playerID);
		cam.GetComponent<CameraMove>().device = null;
		Destroy(cam.gameObject);
	}
}
