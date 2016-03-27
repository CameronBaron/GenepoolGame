using UnityEngine;
using System.Collections;
using InControl;

public class PlayerDeath : MonoBehaviour
{
	public float vibrateTimer = 0.5f;
	public float respawnTimer = 2.0f;

	private Player player { get { return gameObject.GetComponent<Player>(); } }
	private InputDevice device;
	
	void Start ()
	{
		device = player.Device;
	}

	public void DieAndRespawn()
	{
		//do things
		//disable controls		
		//player.deaths += 1;
		player.disableControls = true;

		//fade out quickly

		player.transform.position = new Vector3(0, 1000);
		player.GetComponent<Collider>().enabled = false;
		//couroutine
		StartCoroutine(RespawnTimer());
		StartCoroutine(VibrateTimer());
		//respawn
	}

	IEnumerator RespawnTimer()
	{
		//gameObject.GetComponentInParent<Renderer>().enabled = false;
		yield return new WaitForSeconds(respawnTimer);

		player.health.currentHP = player.health.maxHP;
		transform.position = player.spawnPos.transform.position;
		transform.rotation = Quaternion.identity;
		player.disableControls = false;
		player.GetComponent<Collider>().enabled = true;
		//gameObject.GetComponentInParent<Renderer>().enabled = false;
	}

	IEnumerator VibrateTimer()
	{
		device.Vibrate(2);
		yield return new WaitForSeconds(vibrateTimer);
		device.StopVibration();
	}
}
