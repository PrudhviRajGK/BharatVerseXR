using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVoiceButton : MonoBehaviour
{
    public AudioSource audioSource;        // Assign the AudioSource in Inspector
    public AudioClip archismomVoiceClip;   // Assign archismomvoice.wav in Inspector
    public Button playButton;              // Assign your UI Button in Inspector

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayVoiceClip);
        }
    }

    void PlayVoiceClip()
    {
        if (audioSource != null && archismomVoiceClip != null)
        {
            audioSource.clip = archismomVoiceClip;
            audioSource.Play();
        }
    }
}
