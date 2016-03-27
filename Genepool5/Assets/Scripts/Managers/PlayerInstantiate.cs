using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
	public GameObject ChickenPrefab;
	public GameObject RhinoPrefab;
	public GameObject SharkPrefab;
	public GameObject TigerPrefab;
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
			var player = Instantiate(SelectedChar(GM.chosenType[i + 1]), spawn.transform.position, Quaternion.identity) as GameObject;
			player.GetComponent<Player>().Device = GM.devices[i];
			SetSpawn(player, i + 1, spawn);
			player.tag = "Player";
			if (player.GetComponent<Player>().indicator != null)
			{
				player.GetComponent<Player>().indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", colours[i]);
				player.GetComponent<Player>().colour = colours[i];
            }
			GM.players.Add(player);
			DontDestroyOnLoad(player);
		}
	}

	GameObject SelectedChar(BasePlayer.PLAYERTYPE type)
	{
		switch (type)
		{
			case BasePlayer.PLAYERTYPE.SHARK: return SharkPrefab;
			case BasePlayer.PLAYERTYPE.TIGER: return TigerPrefab;
			case BasePlayer.PLAYERTYPE.RHINO: return RhinoPrefab;
			default: return ChickenPrefab;
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

	private void SetSpawn(GameObject p, int id, GameObject spawn)
	{
		p.GetComponent<Player>().playerID = id;
		p.GetComponent<Player>().spawnPos = spawn;
	}
}
