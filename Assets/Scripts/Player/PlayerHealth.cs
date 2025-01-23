using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth; // Current health of the player
    public GameObject explosionPrefab;

    public SpriteRenderer spriteRenderer; // Assign player's sprite renderer in the Inspector
    public float flashDuration = 0.1f; // Time for each flash
    public int flashCount = 3; // Number of flashes

    public Slider healthSlider; // Reference to the health slider UI
    public GameOverScreen gameOverManager; // Reference to the GameOverManager
    public Wave waveManager; // Reference to the WaveManager for tracking the current wave

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Set initial health value in the slider
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth; // Normalize health to slider range (0 to 1)
        }
    }

    private void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.Play("die");
        }

        // Trigger the Game Over screen
        if (gameOverManager != null && waveManager != null)
        {
            gameOverManager.ShowGameOverScreen(waveManager.GetCurrentWave());
        }

        // Disable the player or handle other death behaviors
        gameObject.SetActive(false);
    }

    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red; // Flash red
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white; // Reset to white
            yield return new WaitForSeconds(flashDuration);
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log("Player's health fully restored!");
    }
}
