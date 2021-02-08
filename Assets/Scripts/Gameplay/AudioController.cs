using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoSingleton<AudioController>
{
    public AudioSource AudioSource2D;

    public void PlayAudio(AudioObject _audioObj)
    {
        if (_audioObj != null)
        {
            AudioClip clip = _audioObj.GetClip();
            if (clip == null)
            {
                Debug.LogError("No Clip Found To Play!");
                return;
            }
            AudioSource2D.clip = clip;
            AudioSource2D.loop = _audioObj.Looping;
            AudioSource2D.Play();
        }
    }
}