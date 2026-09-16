using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Reference to your UI panel

    public void SetupGameOver()
    {
        gameOverPanel.SetActive(true); // Show the game over screen
        Time.timeScale = 0f;          // Freeze game time and movement
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;          // Reset game time back to normal
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
