using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip biteSound;
    public AudioClip dashSound;
    public AudioClip damageSound;
    public AudioClip[] moveSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBiteSound()
    {
        audioSource.PlayOneShot(biteSound);
    }

    public void PlayDashSound()
    {
        audioSource.PlayOneShot(dashSound);
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSound);
    }

    public void PlayMoveSound()
    {
        AudioClip randomMoveSound = GetRandomClip(moveSound); 
        audioSource.PlayOneShot(randomMoveSound); 
    }

    // Helper function to get a random clip from an array
    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips.Length == 0) return null; // Return if the array is empty
        int randomIndex = Random.Range(0, clips.Length); // Get a random index
        return clips[randomIndex]; // Return the random clip
    }
}
