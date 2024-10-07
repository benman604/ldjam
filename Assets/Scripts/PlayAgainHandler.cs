using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainHandler : MonoBehaviour
{
    // Example function to load a new scene
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
