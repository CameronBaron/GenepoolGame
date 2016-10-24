using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
	public GameObject PlayerPrefab;
	public Color[] colours = new Color[] { Color.yellow, Color.red, Color.blue, Color.green };
	public float respawnTimer = 2.0f;

	private GameObject[] respawns;

	GameManager GM;

	// Use this for initialization
	void Start ()
	{
		GM = GameManager.Instance;

		respawns = GameObject.FindGameObjectsWithTag("Respawn");

		for (int i = 0; i < GM.isPlaying.Count; i++)
		{
			GameObject spawn = GetSpawn(i + 1);
			var player = Instantiate(PlayerPrefab, spawn.transform.position, Quaternion.identity) as GameObject;
			player.GetComponent<PlayerController>().SetupPlayer(colours[i], i, spawn.transform);
			GM.players.Add(player);
		}
	}

	private GameObject GetSpawn(int id)
	{
		for (int i = 0; i < respawns.Length; i++)
		{
			if (int.Parse(respawns[i].name[respawns[i].name.Length - 1].ToString()) == id)
			{
				return respawns[i];
			}
		}
		return null;
	}
}
