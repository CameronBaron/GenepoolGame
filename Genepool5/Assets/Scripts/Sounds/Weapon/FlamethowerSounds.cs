using UnityEngine;
using System.Collections;
using InControl;

public class FlamethowerSounds : MonoBehaviour 
{
    private InputDevice device;

    public AudioClip[] gunSounds = new AudioClip[3];
    public float volume = 1;
    private int audioSourceID = -1;
    private SoundMode eSoundMode = SoundMode.IDLE;

    private float delay = 1;
    private float counter = 0;
    private bool start = false;

    private enum SoundMode
    {
        BEGIN,
        LOOP,
        IDLE,
    }

	// Use this for initialization
	void Start () 
    {
        device = GetComponentInParent<Player>().Device;
        eSoundMode = SoundMode.IDLE;
	}
	
	// Update is called once per frame
	void Update () 
    {
        counter += Time.deltaTime;
        //START
        if (device.RightTrigger.IsPressed)
        {
            if(eSoundMode == SoundMode.IDLE)
            {
                StartSound();
            }

            //LOOP
            if(eSoundMode == SoundMode.BEGIN && counter > delay)
            {
                LoopSound();
            }
        }

        //END
        if ((!(device.RightTrigger.IsPressed)) && start == true)
        {
            EndSound();
        }
	}

    public void StartSound()
    {
        SoundManager.StopAudio(audioSourceID);
        eSoundMode = SoundMode.BEGIN;
        audioSourceID = SoundManager.PlaySingle(volume, gunSounds[0], false);
        start = true;
        counter = 0.0f;
    }

    public void LoopSound()
    {
        eSoundMode = SoundMode.LOOP;
        audioSourceID = SoundManager.PlaySingle(volume, gunSounds[1], true);
    }

    public void EndSound()
    {
        eSoundMode = SoundMode.IDLE;
        SoundManager.StopAudio(audioSourceID);
        audioSourceID = SoundManager.PlaySingle(volume, gunSounds[2], false);
        start = false;
    }
}

