using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class GunSight : MonoBehaviour
{
	LineRenderer lineR;
	RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
		lineR = GetComponent<LineRenderer>();
		lineR.receiveShadows = false;
		lineR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lineR.SetWidth(0.05f, 0.05f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		lineR.SetPosition(0, transform.position);
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			lineR.SetPosition(1, hit.point);

			// Hit point + (reflection dir * draw distance)
			//lineR.SetPosition(2, );
		}
	}
}
