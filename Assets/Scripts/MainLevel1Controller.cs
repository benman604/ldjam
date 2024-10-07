using UnityEngine;

public class MainLevel1Controller : MonoBehaviour
{
    void Start()
    {
        // Reset the timer at the start of MainLevel1
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
        }
    }

    void OnDisable()
    {
        // Stop the timer when the scene is about to unload
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.StopTimer();
        }
    }
}
