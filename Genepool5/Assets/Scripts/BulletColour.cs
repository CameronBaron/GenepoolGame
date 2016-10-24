using UnityEngine;
using System.Collections;

public class BulletColour : MonoBehaviour
{
	private PlayerController player;
	// Use this for initialization
	void Start ()
	{
		player = GetComponentInParent<Gun>().GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Light>().color = player.GetComponent<PlayerController>().indicator.GetComponent<Renderer>().material.color;
    }
}
