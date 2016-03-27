using UnityEngine;
using System.Collections;

public class ShootScript1 : MonoBehaviour {

	public Transform spawn;
	public Rigidbody projectile;
	public float projSpeed = 30;

	public float delay = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Fire1")) {
			Delay ();
			Rigidbody proj = Instantiate (projectile, spawn.transform.position, spawn.transform.rotation) as Rigidbody;
			proj.velocity = transform.TransformDirection (new Vector3 (0, 0, projSpeed));
		}
	}
	IEnumerator Delay () {
		yield return new WaitForSeconds (delay);
	}
}
