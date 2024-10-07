using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip biteSound;
    public AudioClip dashSound;
    public AudioClip spitSound;
    public AudioClip damageSound;

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

    public void PlaySpitSound()
    {
        audioSource.PlayOneShot(spitSound);
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSound);
    }
}
