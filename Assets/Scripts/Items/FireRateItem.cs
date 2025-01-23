using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateItem : MonoBehaviour
{
    public float increasedFireRate = 0.2f; // Fire rate when the item is picked up
    public float duration = 5f; // Duration of the fire rate increase

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                if (playerShooting.getCurrentFireRate() == increasedFireRate)
                {
                    Debug.Log("Fire Rate is already Increas");
                }
                else
                {
                    playerShooting.IncreaseFireRate(increasedFireRate, duration);
                    AudioManager.instance.Play("Health");
                    // Destroy the item after it's collected
                    Destroy(gameObject);
                }
               
            }

            
        }
    }
}
