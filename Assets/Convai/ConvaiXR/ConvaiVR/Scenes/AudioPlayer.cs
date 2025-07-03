using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public enum AudioOptions
    {
        Odisha,
        Charminar
    }

    public AudioOptions selectedAudio;
    public AudioSource audioSource;

    public AudioClip odishaClip;
    public AudioClip charminarClip;

    public void PlaySelectedAudio()
    {
        if (!audioSource) return;

        switch (selectedAudio)
        {
            case AudioOptions.Odisha:
                audioSource.clip = odishaClip;
                break;
            case AudioOptions.Charminar:
                audioSource.clip = charminarClip;
                break;
        }

        if (audioSource.clip != null)
            audioSource.Play();
    }
}
