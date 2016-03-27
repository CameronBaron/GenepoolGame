using UnityEngine;
using System.Collections;

public class PlayerTwoScript : MonoBehaviour {

	public float currentHealth = 100f;

	public float maxHealth = 100f;

	public float moveSpeed;
	public float maxSpeed;

	private Rigidbody rb;
	private Vector3 input;

	public Vector3 spawn;

	// Use this for initialization
	void Start () {

		spawn = transform.position;

		rb = GetComponent<Rigidbody> ();
	
	}

	void Update () {
		
		if (currentHealth <= 0) {
			Die ();
		}
	}

	void FixedUpdate () {

		TwinStickController ();
		//KeyboardController ();
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Bullet") {
			currentHealth -= 1;
		}
	}

	void OnTriggerEnter (Collider other) {
	}

	void TwinStickController () {
		Vector3 input = new Vector3 (Input.GetAxisRaw ("P2LeftStickX"), 0, Input.GetAxisRaw ("P2LeftStickY"));
		Vector3 input2 = new Vector3 (Input.GetAxisRaw ("P2RightStickX"), 0, Input.GetAxisRaw ("P2RightStickY"));


		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (input * moveSpeed);
		}
		
		if (input2.x == 0 && input2.z == 0) {
			
		}else{
			transform.rotation = Quaternion.LookRotation (input2);
		}
	}

	void KeyboardController () {


		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (input * moveSpeed);
		}
		
		if (input.x == 0 && input.z == 0) {
			
		}else{
			transform.rotation = Quaternion.LookRotation (input);
		}
	}

	public void Die () {
		transform.position = spawn;
		currentHealth = 100f;
	}
}
