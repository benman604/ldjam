using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // This method will be called to exit the game
    public void Exit()
    {
        // For editor testing, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // For a built application, close the application
            Application.Quit();
        #endif
    }
}
