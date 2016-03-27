using UnityEngine;

public class LauncherSounds : MonoBehaviour 
{
    public AudioClip gunSound;
    public float volume = 1;
    [HideInInspector]   public Gun gun;

    private int audioSourceID = 0;

	// Use this for initialization
	void Start () 
    {
        gun = GetComponentInParent<Gun>();
	}

    void Update()
    {
        if (gun.shooting)
        {
            audioSourceID = SoundManager.PlaySingle(volume, gunSound, false);
        }
    }
}
