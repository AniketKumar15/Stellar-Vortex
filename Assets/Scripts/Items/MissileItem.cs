using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItem : MonoBehaviour
{
    public float duration = 5f; // Duration for which the player shoots missiles

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                if (playerShooting.getCurrentProjectile() == playerShooting.missilePrefab)
                {
                    Debug.Log("Aready Missile Active");
                }
                else
                {
                    playerShooting.ChangeToMissile(duration);
                    AudioManager.instance.Play("Health");
                    // Destroy the item after it's collected
                    Destroy(gameObject);
                }
                
            }

            
        }
    }
}
