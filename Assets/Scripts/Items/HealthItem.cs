using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a PlayerHealth component
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Restore the player's health to full
            if (playerHealth.currentHealth == playerHealth.maxHealth)
            {
                Debug.Log("Player Health is full");
            }
            else
            {
                playerHealth.RestoreFullHealth();
                AudioManager.instance.Play("Health");
                // Destroy the health item after use
                Destroy(gameObject);
            }
            
        }
    }
}
