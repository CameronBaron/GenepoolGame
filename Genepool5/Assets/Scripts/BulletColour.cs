using UnityEngine;
using System.Collections;

public class BulletColour : MonoBehaviour
{
	private Player player;
	// Use this for initialization
	void Start ()
	{
		player = GetComponentInParent<Gun>().GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Light>().color = player.GetComponent<Player>().indicator.GetComponent<Renderer>().material.color;
    }
}
