using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Example function to load a new scene
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainLevel 1");
    }
}
