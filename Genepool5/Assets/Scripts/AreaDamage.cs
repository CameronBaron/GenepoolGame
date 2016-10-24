using UnityEngine;
using System.Collections;

public class AreaDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Health>().AdjustCurrentHP(-1);
		}
	}
}
