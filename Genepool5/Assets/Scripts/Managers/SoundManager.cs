//=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
//                          Sound Manager
//                      Author: Eli Iannuzzo
//                      Date:   20/10/2015
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//  Description:
//      This is the Sound Manager Script, it makes audio related
//      code a complete breeze.
//      -Playing Audio
//      -Volume Control
//      -Stoping Audio & more...
//
//=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    //Publics
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameoverMusic;

    public static SoundManager instance = null;
    public static float lowPitchRange = .95f;
    public static float highPitchRange = 1.05f;

    //Privates
    private static List<AudioSource> audioSrc = new List<AudioSource>();

    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
        }

		instance = this;

		//Don't destroy when loading new scene please
		DontDestroyOnLoad(gameObject);


		//Setting the currect scene music depending on the gamestate.

		for (int i = 0; i < 20; ++i)
        {
            audioSrc.Add(gameObject.AddComponent<AudioSource>());
            audioSrc[i].loop = false;
            audioSrc[i].playOnAwake = false;
        }
    }

    private static int FindFreeAudioSource()
    {
        for(int i = 0; i < audioSrc.Count; ++i)
        {
			if (audioSrc[i] == null)
			{
				audioSrc.RemoveAt(i);
				i--;
			}
            else if(!audioSrc[i].isPlaying)
                return i;
        }

        AudioSource freeSource = instance.gameObject.AddComponent<AudioSource>();
        audioSrc.Add(freeSource);
        freeSource.loop = false;
        freeSource.playOnAwake = false;
        return audioSrc.Count - 1;
    }

    //Used to play single sound clips.
    public static int PlaySingle(AudioClip clip, bool looping = false)
    {
        int index = FindFreeAudioSource();
        AudioSource freeSource = audioSrc[index];
        freeSource.clip = clip;
        freeSource.loop = looping;
        //freeSource.spatialBlend = 0.5f;
        freeSource.Play();

        return index;
    }

    public static int PlaySingle(float volume, AudioClip clip, bool looping = false)
    {
        int index = FindFreeAudioSource();
        AudioSource freeSource = audioSrc[index];
        freeSource.clip = clip;
        freeSource.loop = looping;
        freeSource.volume = volume;
        freeSource.Play();

        return index;
    }

    public static AudioSource GetAudioSource(int id)
    {
        return audioSrc[id];
    }

    public static bool IsPlaying(int id)
    {
        return audioSrc[id].isPlaying;
    }

    public static int PlayRandom(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        int index = FindFreeAudioSource();
        AudioSource freeSource = audioSrc[index];
        freeSource.pitch = randomPitch;
        freeSource.clip = clips[randomIndex];
        freeSource.Play();

        return index;
    }

    public static void StopAudio(int id)
    {
        if(id >= 0)
            audioSrc[id].Stop();
    }

    public static void KeepOne(AudioClip clip)
    {
        bool found = false;

        for (int i = 0; i < audioSrc.Count; i++)
        {
            if (audioSrc[i].clip == clip)
            {
                if (found == true)
                {
                    audioSrc[i].Stop();
                    audioSrc[i].clip = null;
                }
                else
                {
                    found = true;
                }
            }
        }
    }

    public static int GetAudioID(AudioClip clip)
    {
        for (int i = 0; i < audioSrc.Count; i++)
        {
            if (audioSrc[i].clip == clip)
            {
                return i;
            }
        }
        return -1;
    }

    public static void SceneMusic(GameState gameState)
    {
        if (gameState == GameState.MENU)
        {
            SoundManager.PlaySingle(0.4f, instance.menuMusic, true);
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.gameplayMusic));
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.gameoverMusic));
        }
        else if (gameState == GameState.PLAY)
        {
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.menuMusic));
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.gameoverMusic));
            SoundManager.PlaySingle(0.2f, instance.gameplayMusic, true);
        }
        else if (gameState == GameState.WIN)
        {
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.menuMusic));
            SoundManager.StopAudio(SoundManager.GetAudioID(instance.gameplayMusic));
            SoundManager.PlaySingle(0.4f, instance.gameoverMusic, true);
        }
    }
}