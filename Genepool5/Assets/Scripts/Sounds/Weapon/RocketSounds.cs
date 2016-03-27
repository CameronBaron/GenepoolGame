using UnityEngine;

public class RocketSounds : MonoBehaviour
{
    public AudioClip gunSound;
    public float volume = 1;
    private int audioSourceID = 0;

    // Use this for initialization
    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSourceID = SoundManager.PlaySingle(volume, gunSound, false);
    }
}