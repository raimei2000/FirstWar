using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AudioSource.Play() is used to start playing the audio clip. (BGM)
//AudioSource.Stop() is used to stop playing the audio clip.
//AudioSource.Pause() is used to pause the audio clip.
//AudioSource.UnPause() is used to resume playing the audio clip after it has been paused.
//AudioSource.loop is used to set whether the audio clip should loop when it reaches the end.
//AudioSource.volume is used to set the volume of the audio clip.
//AudioSource.pitch is used to set the pitch of the audio clip.
//AudioSource.PlayOneShot(AudioClip clip) is used to play a single instance of the audio clip without interrupting any currently playing audio.


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    AudioSource source;

    public AudioClip[] audios = new AudioClip[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        source = GetComponent<AudioSource>();

        audios[0] = Resources.Load<AudioClip>("Hit");
    }
    
    public void AudioStart(int value)
    {
        source.PlayOneShot(audios[value]);
    }
}
