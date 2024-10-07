using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign in the Inspector
    public float delayBeforeTransition = 2f; // Delay before transitioning to the next scene

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();

            // Subscribe to the loop point reached event to trigger scene transition
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("Video Player not assigned!");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(delayBeforeTransition);
        
        // Load the Victory scene
        SceneManager.LoadScene("Victory"); // Replace with your actual Victory scene name
    }
}
