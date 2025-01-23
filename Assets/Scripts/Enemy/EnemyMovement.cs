using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // Speed of the enemy
    private Transform playerTransform; // Reference to the player's Transform
    public int damage = 10;

    private void Start()
    {
        // Find the player in the scene and get its Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
