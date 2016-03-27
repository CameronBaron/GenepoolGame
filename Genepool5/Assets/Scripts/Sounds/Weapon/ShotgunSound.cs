using UnityEngine;
using System.Collections;
using InControl;

public class ShotgunSound : MonoBehaviour 
{
    private InputDevice device;

    public AudioClip gunSound;
    public float delay = 1;
    public float volume = 1;

    private int audioSourceID = 0;
    private float counter = 0;

	void Start () 
    {
        counter = delay;
		device = GetComponentInParent<Player>().Device;
	}
	
	void Update () 
    {
        counter += Time.deltaTime;

        if (device.RightTrigger.IsPressed)
        {
            if (counter > delay)
            {
                counter = 0;
                audioSourceID = SoundManager.PlaySingle(volume, gunSound, false);
            }
        }
	}
}
