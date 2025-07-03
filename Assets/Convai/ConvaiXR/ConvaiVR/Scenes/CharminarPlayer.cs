using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharminarPlayer : MonoBehaviour
{
    public AudioClip charminarClip; // Assign in Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = charminarClip;
        audioSource.playOnAwake = false;
    }

    // Call this on button press
    public void PlayAudio()
    {
        if (charminarClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not assigned!");
        }
    }
}
