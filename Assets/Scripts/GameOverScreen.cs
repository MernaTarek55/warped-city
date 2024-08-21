using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverCanvas; 

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}
