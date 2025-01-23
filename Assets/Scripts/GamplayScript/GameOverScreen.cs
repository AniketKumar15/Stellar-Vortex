using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen; // Reference to the Game Over screen panel
    public Text highestWaveText; // UI Text to display the highest wave survived
    public Text currentWaveText; // UI Text to display the current wave survived

    private int highestWave = 0;

    private void Start()
    {
        // Load the highest wave survived from PlayerPrefs
        highestWave = PlayerPrefs.GetInt("HighestWave", 0);
        UpdateUI(0); // Initialize UI with a default wave count of 0
    }

    public void ShowGameOverScreen(int currentWave)
    {
        // Update and save the highest wave survived
        if (currentWave > highestWave)
        {
            highestWave = currentWave;
            PlayerPrefs.SetInt("HighestWave", highestWave);
            PlayerPrefs.Save();
        }

        // Update the UI elements
        UpdateUI(currentWave);

        // Activate the Game Over screen
        gameOverScreen.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    private void UpdateUI(int currentWave)
    {
        highestWaveText.text = "Highest Wave: " + highestWave;
        currentWaveText.text = "Current Wave: " + currentWave;
    }

    public void RetryGame()
    {
        // Reload the current scene to restart the game
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        // Load the main menu scene
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu");
    }
}
