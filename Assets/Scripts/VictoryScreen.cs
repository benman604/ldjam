using UnityEngine;
using TMPro; // Use the TextMesh Pro namespace

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro component

    void Start()
    {
        if (GameTimer.Instance != null)
        {
            float finalTime = GameTimer.Instance.GetElapsedTime();
            DisplayTime(finalTime);
        }
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
