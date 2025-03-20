using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Method to reload the current scene
    public void ReloadScene()
    {
        // Get the currently active scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene by its index
        SceneManager.LoadScene(currentSceneIndex);
    }
}
