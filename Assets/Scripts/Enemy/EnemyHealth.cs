using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth;  // Current health of the enemy
    public GameObject explosionPrefab;

    public delegate void DeathAction(); // Declare a delegate (event) for enemy death
    public event DeathAction OnDeath;

    private SpriteRenderer spriteRenderer; // Assign player's sprite renderer in the Inspector
    public float flashDuration = 0.1f; // Time for each flash
    public int flashCount = 3; // Number of flashes

    public GameObject coinPrefab; // Coin prefab to spawn
    public int coinsToSpawn = 1; // Number of coins to spawn

    //Items/PowerUp
    public int randomNum;
    public GameObject healthItem;
    public GameObject FireRateItem;
    public GameObject MissileItem;

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        //Items/PowerUp
        randomNum = Random.Range(0, 20);
    }

    public void TakeDamage(int damage)
    {
        // Reduce health
        currentHealth -= damage;
        StartCoroutine(FlashEffect());
        // Check if health is 0 or less
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        //Items/PowerUp
        if (randomNum == 10 && healthItem != null)
        {
            Instantiate(healthItem, transform.position, Quaternion.identity);
            Debug.Log("Health");
        }
        else if (randomNum == 5 && FireRateItem != null)
        {
            Instantiate(FireRateItem, transform.position, Quaternion.identity);
            Debug.Log("FireRate");
        }
        else if (randomNum == 15 && MissileItem != null)
        {
            Instantiate(MissileItem, transform.position, Quaternion.identity);
            Debug.Log("Missile");
        }
        else
        {
            SpawnCoins();
        }


        if (OnDeath != null)
        {
            OnDeath.Invoke(); // Notify that the enemy has died
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.Play("die");
        }
        // Destroy the enemy object
        Destroy(gameObject);
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

    private void SpawnCoins()
    {
        for (int i = 0; i < coinsToSpawn; i++)
        {
            // Instantiate a coin at the enemy's position
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
