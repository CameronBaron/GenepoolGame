using UnityEngine;
using System.Collections;

public class P1ShootScript : MonoBehaviour {

	public Transform spawn;
	public Rigidbody projectile;
	public float projSpeed = 30;

	public float delay;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		Shoot ();
		Debug.Log("bang");
	}

	void Shoot ()
	{
		Rigidbody proj = Instantiate (projectile, spawn.position, spawn.rotation) as Rigidbody;
		proj.velocity = transform.TransformDirection(new Vector3(Random.Range (-1.5f, 1.5f), Random.Range(-1, 1), projSpeed));
	}

	IEnumerator Delay ()
    {
		yield return new WaitForSeconds(delay);
	}
}
