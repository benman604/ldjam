using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SpiderMusicController : MonoBehaviour
{
    public AudioSource spiderMusicAudioSource; // The audio source that plays spider music
    public float detectionRange = 5f; // The range in which the spider can be "heard"
    public float fadeSpeed = 1f; // How fast the volume fades in/out
    public LayerMask spiderLayer; // A layer for the spider objects

    private GameObject player; // The player object
    private float targetVolume = 0f; // The volume we are fading towards
    private float currentVolume = 0f; // The current volume of the audio source

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Find the player by tag
        if (spiderMusicAudioSource == null)
        {
            Debug.LogError("SpiderMusicController: No AudioSource assigned!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Find the closest spider
            GameObject closestSpider = FindClosestSpider();

            if (closestSpider != null)
            {
                float distanceToSpider = Vector3.Distance(player.transform.position, closestSpider.transform.position);

                // If within detection range, calculate volume based on proximity
                if (distanceToSpider < detectionRange)
                {
                    targetVolume = Mathf.Lerp(0f, 1f, (detectionRange - distanceToSpider) / detectionRange);
                }
                else
                {
                    targetVolume = 0f; // Outside range, fade out the sound
                }
            }
            else
            {
                targetVolume = 0f; // No spider detected, fade out the sound
            }

            // Smoothly adjust the current volume to the target volume
            currentVolume = Mathf.Lerp(currentVolume, targetVolume, fadeSpeed * Time.deltaTime);
            spiderMusicAudioSource.volume = currentVolume;
        }
    }

    // Helper method to find the closest spider
    GameObject FindClosestSpider()
    {
        GameObject[] spiders = GameObject.FindGameObjectsWithTag("Spider");
        GameObject closestSpider = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject spider in spiders)
        {
            float distance = Vector3.Distance(player.transform.position, spider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSpider = spider;
            }
        }

        return closestSpider;
    }
}

