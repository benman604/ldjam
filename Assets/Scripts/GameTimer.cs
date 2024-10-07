using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance; // Singleton instance
    private float elapsedTime;
    private bool isTiming;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Public method to reset and start the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isTiming = true;
    }

    void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
