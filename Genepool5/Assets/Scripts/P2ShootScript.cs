using UnityEngine;
using System.Collections;

public class P2ShootScript : MonoBehaviour {

	public Transform spawn;
	public Rigidbody projectile;
	public float projSpeed = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Shoot ();
	}

	void Shoot () {
		if (Input.GetButton ("P2Shoot")) {
			Rigidbody proj = Instantiate (projectile, spawn.position, spawn.rotation) as Rigidbody;
			proj.velocity = transform.TransformDirection (new Vector3 (Random.Range (-3, 3), Random.Range (-5, 8), projSpeed));
		}
	}
}
