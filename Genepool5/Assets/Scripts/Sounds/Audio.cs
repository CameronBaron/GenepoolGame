using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip sound;
    public float volume;

    public void PlayAudio()
    {
        SoundManager.PlaySingle(sound);
    }
}
