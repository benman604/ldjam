using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSFX : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip biteSound;
    public AudioClip dashSound;
    public AudioClip damageSound;
    public AudioClip walkSound;
    public AudioClip deathSound;

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
        audioSource.PlayOneShot(walkSound);
    }

    public void PlayDeathSound() {
        audioSource.PlayOneShot(deathSound);
    }
}
