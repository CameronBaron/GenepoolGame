using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemyPrefab;
	public GameObject[] respawns;
	public float spawnTimer;

	private LinkedList<GameObject> enemies = new LinkedList<GameObject>();
 	private float spawnCounter = 0;

	// Use this for initialization
	void Start ()
	{
		Physics.IgnoreLayerCollision(9, 9);
		respawns = GameObject.FindGameObjectsWithTag("EnemyRespawn");
		foreach (GameObject r in respawns)
		{
			Spawn(r);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		spawnCounter += Time.deltaTime;

		if (spawnCounter >= spawnTimer)
		{
			RandomSpawn();
			spawnCounter = 0;
		}
	}

	public void Spawn(GameObject spawnPos)
	{
		Object enemy = Instantiate(enemyPrefab, spawnPos.transform.position, enemyPrefab.transform.rotation);
		enemies.AddLast((GameObject)enemy);
	}

	public void RandomSpawn()
	{
		Vector3 newPos = respawns[Random.Range(0, respawns.Length)].transform.position;
		Object enemy = Instantiate(enemyPrefab, newPos, enemyPrefab.transform.rotation);
		enemies.AddLast((GameObject)enemy);
	}

	public void RemoveEnemy()
	{

	}
}
