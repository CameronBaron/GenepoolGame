using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float seekRange = 25f;
	public int fleePoint;           // Health value to start fleeing
	public float meleeRange = 1;        // For stopping the AI getting too close to the player.
	public float speed = 15;
	public Transform respawn;

	private NavMeshAgent agent;
	private GameObject target;
	private float closeToTarget = 5f;
	private float accel = 20f;

	// Use this for initialization
	void Start ()
	{
		// Reference vars.
		agent = GetComponent<NavMeshAgent>();
		// Set speed.
		agent.speed = speed;

		// Get target and set path.
		target = GetClosestPlayer();
		if (target != null)
			agent.destination = target.transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		// If I have a path, set accel.
		if (agent.hasPath)
		{
			agent.acceleration = accel;
		}

		if (target != null)
		{
			Seek();
		}
	}

	void Seek()
	{
		if (agent.remainingDistance < meleeRange)
		{
			agent.velocity = Vector3.zero;
		}

		target = GetClosestPlayer();
		agent.destination = target.transform.position;
	}

	/// <summary>
	/// Searchs through all players and uses the closest as the target.
	/// </summary>
	/// <returns></returns>
	GameObject GetClosestPlayer()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Player");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	void OnCollisionEnter(Collision col)
	{

	}
}
