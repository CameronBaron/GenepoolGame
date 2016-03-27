using UnityEngine;
using System.Collections;
using InControl;

public class FullAutoSound : MonoBehaviour 
{
    private InputDevice device;

    public AudioClip gunSound;
    public float delay = 1;
    public float volume = 1;

    private int audioSourceID = 0;
    private float counter = 0;

	// Use this for initialization
	void Start () 
    {
		device = GetComponentInParent<Player>().Device;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (device.RightTrigger.IsPressed)
        {
            counter += Time.deltaTime;
            if (counter > delay)
            {
                counter = 0;
                audioSourceID = SoundManager.PlaySingle(volume, gunSound, false);
            }
        }

        if (!(device.RightTrigger.IsPressed))
        {
            counter = 0;
        }
	}
}
