using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string previousScene;

    public static void LoadScene(string sceneName)
    {
        // Store the current scene's name as the previous scene
        previousScene = SceneManager.GetActiveScene().name;
        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }

    // Method to go back to the previous scene
    public static void GoBack()
    {
        // Check if there is a previous scene stored
        if (!string.IsNullOrEmpty(previousScene))
        {
            // Load the previous scene
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene found!");
        }
    }
}
